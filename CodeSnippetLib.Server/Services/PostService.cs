using CodeSnippetLib.Server.Data;
using CodeSnippetLib.Server.DTOs.Post;
using CodeSnippetLib.Server.Helpers.Mappers;
using CodeSnippetLib.Server.Interfaces.Repos;
using CodeSnippetLib.Server.Interfaces.Services;
using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postRepo;
        private readonly ITagRepo _tagRepo;
        private readonly ApplicationDbContext _context;

        public PostService(IPostRepo postRepo, ITagRepo tagRepo, ApplicationDbContext context)
        {
            _postRepo = postRepo;
            _tagRepo = tagRepo;
            _context = context;
        }

        public async Task<(bool success, string message)> CreatePost(NewPostDto dto, string userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                List<Tag> compiledTagList = await _tagRepo.GetTagList(dto.Tags);

                Post newPost = new Post()
                {
                    Title = dto.Title,
                    Slug = dto.Title.ToLowerInvariant().Replace(" ", "-"),
                    Content = dto.Content,
                    AuthorId = userId
                };

                await _postRepo.CreatePost(newPost);

                if (compiledTagList.Any())
                {
                    await _tagRepo.MapTagsToPost(newPost.Id, compiledTagList);
                }

                await transaction.CommitAsync();
                return (true, "Post successfully created");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, ex.Message ?? "An error occured creating a new post");
            }            
        }

        public async Task<Post?> GetPostBySlug(string slug)
        {
            return await _postRepo.GetPostBySlug(slug);
        }

        public async Task<List<TagToPost>> GetPostsByTagId(int tagId)
        {
            return await _postRepo.GetPostsByTagId(tagId);
        }

        public async Task<(List<PostDto> postDtos, int totalCount)> GetPostSelection(int skip, int take, string? search, string? tag)
        {
            var (posts, count) = await _postRepo.GetPostSelection(skip, take, search, tag);

            return (posts.Select(p => p.ToPostDto()).ToList(), count);
        }

        public async Task<(bool success, string message)> UpdatePost(UpdatePostDto dto, string slug)
        {
            //Get target post
            Post? currentPost = await GetPostBySlug(slug);

            if (currentPost == null)
            {
                return (false, "Post could not be found");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                //Update post metadata
                currentPost.UpdatePost(dto);
                await _postRepo.UpdatePost(currentPost);

                //Update Tags
                List<Tag> tagsForUpdatedPost = await _tagRepo.GetTagList(dto.Tags); //Create new tags in Tags table and compile list for updated post
                
                //Clean tags from original post
                //Check tags in current post against new updated tags and filter list to tags that need to be removed
                List<int> updatedPostTagIds = tagsForUpdatedPost.Select(t => t.Id).ToList();

                List<TagToPost> tagsToRemove = currentPost.TagToPosts
                    .Where(ttp => !updatedPostTagIds.Contains(ttp.TagId))
                    .ToList();

                foreach (TagToPost ttp in tagsToRemove)
                {
                    await _tagRepo.DeleteTagToPost(ttp);

                    //Test if any posts exist for tag - if empty remove tag
                    List<TagToPost> postsForCurrentTag = await GetPostsByTagId(ttp.TagId);

                    if (!postsForCurrentTag.Any())
                    {
                        await _tagRepo.DeleteTag(ttp.Tag);
                    }
                }

                //Filter out existing tags so duplicate TTP models aren't created
                HashSet<int> originalTagIds = currentPost.TagToPosts
                    .Select(ttp => ttp.TagId)
                    .ToHashSet();

                List<Tag> cleanedTagList = tagsForUpdatedPost
                    .Where(t => !originalTagIds.Contains(t.Id))
                    .ToList();

                //Map new tags
                if (cleanedTagList.Any())
                {   
                    await _tagRepo.MapTagsToPost(currentPost.Id, cleanedTagList);
                }

                //Save and commit transaction
                await transaction.CommitAsync();
                return (true, "Post successfully updated");

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, ex.Message ?? "An error occured while updating post");
            }
        }

        public async Task<(bool success, string message)> DeletePost(string slug)
        {
            Post? currentPost = await GetPostBySlug(slug);

            if (currentPost == null) return (false, "Post could not be found in database");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                List<TagToPost> currentPostTagJoins = currentPost.TagToPosts.ToList();
                List<Tag> associatedTags = currentPostTagJoins.Select(ttp => ttp.Tag).ToList();

                //Clean up TagToPost joins for post
                foreach (TagToPost ttp in currentPostTagJoins)
                {
                    await _tagRepo.DeleteTagToPost(ttp);
                }

                await _postRepo.DeletePost(currentPost);
                
                //Ensure posts exist for each tag - Clean empty tags
                foreach (Tag t in associatedTags)
                {
                    List<TagToPost> remainingPostsForTag = await GetPostsByTagId(t.Id);

                    if (!remainingPostsForTag.Any())
                    {
                        await _tagRepo.DeleteTag(t);
                    }
                }
                
                await transaction.CommitAsync();
                return (true, "Post deleted successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, ex.Message ?? "An error occured white deleting post");
            }
        }
    }
}

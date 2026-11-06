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

        public async Task<int> GetPostCount()
        {
            return await _postRepo.GetPostCount();
        }

        public async Task<List<TagToPost>> GetPostsByTagId(int tagId)
        {
            return await _postRepo.GetPostsByTagId(tagId);
        }

        public async Task<List<PostDto>> GetPostSelection(int skip, int take)
        {
            List<Post> posts = await _postRepo.GetPostSelection(skip, take);

            return posts.Select(p => p.ToPostDto()).ToList();
        }
    }
}

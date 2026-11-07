using CodeSnippetLib.Server.Data;
using CodeSnippetLib.Server.Interfaces.Repos;
using CodeSnippetLib.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippetLib.Server.Repos
{
    public class PostRepo : IPostRepo
    {
        private readonly ApplicationDbContext _context;

        public PostRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreatePost(Post newPost)
        {
            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();            
        }

        public async Task<Post?> GetPostBySlug(string slug)
        {
            return await _context.Posts
                .Include(p => p.TagToPosts)
                .ThenInclude(ttp => ttp.Tag)
                .FirstOrDefaultAsync(p => p.Slug == slug);
        }

        public async Task<List<TagToPost>> GetPostsByTagId(int tagId)
        {
            return await _context.TagsToPost
                .Where(ttp => ttp.TagId == tagId)
                .Include(ttp => ttp.Post)
                .ToListAsync();
        }

        public async Task<(List<Post> posts, int totalCount)> GetPostSelection(int skip, int take, string? search, string? tag)
        {
            var query = _context.Posts.AsNoTracking().AsQueryable();

            //Handle tag filtering
            if (!string.IsNullOrWhiteSpace(tag))
            {
                string cleanedTag = tag.Trim();
                query = query.Where(p => p.TagToPosts.Any(ttp => ttp.Tag.Name == cleanedTag));
            }

            //Handle search query if available
            if (!string.IsNullOrWhiteSpace(search))
            {
                string fullPhrase = search.Trim();
                string[] words = fullPhrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var candidateQuery = query.Where(p =>
                p.Title.Contains(fullPhrase) ||
                p.Content.Contains(fullPhrase) ||
                p.TagToPosts.Any(ttp => ttp.Tag.Name.Contains(fullPhrase)) ||
                words.Any(w => 
                    p.Title.Contains(w) || 
                    p.Content.Contains(w) ||
                    p.TagToPosts.Any(ttp => ttp.Tag.Name.Contains(w))
                    )                
                );

                //Score search results based on where searched words exist
                var scoredQuery = candidateQuery.Select(p => new
                {
                    Post = p,
                    Score =
                        (p.Title.Contains(fullPhrase) ? 100 : 0) +
                        (p.Content.Contains(fullPhrase) ? 40 : 0) +
                        (words.Any(w => p.Title.Contains(w)) ? 15 : 0) +
                        (words.Any(w => p.TagToPosts.Any(ttp => ttp.Tag.Name.Contains(w))) ? 10 : 0) +
                        (words.Any(w => p.Content.Contains(w)) ? 3 : 0)
                });

                int totalCount = await scoredQuery.CountAsync();

                List<Post> queriedPosts = await scoredQuery
                    .OrderByDescending(x => x.Score)
                    .ThenByDescending(x => x.Post.CreatedAt)
                    .Select(x => x.Post)
                    .Skip(Math.Max(0, skip))
                    .Take(Math.Clamp(take, 1, 20))
                    .Include(p => p.TagToPosts)
                    .ThenInclude(ttp => ttp.Tag)
                    .ToListAsync();

                return (queriedPosts, totalCount);
            }

            //Executes if search query isn't available, simply takes batch for front end pagination
            int totalPosts = await query.CountAsync();

            List<Post> posts =  await query
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Skip(Math.Max(0, skip))
                .Take(Math.Clamp(take, 1, 20))
                .Include(p => p.TagToPosts)
                .ThenInclude(ttp => ttp.Tag)
                .ToListAsync();

            return (posts, totalPosts);
        }
    }
}

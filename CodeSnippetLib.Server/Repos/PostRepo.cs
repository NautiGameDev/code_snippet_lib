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
    }
}

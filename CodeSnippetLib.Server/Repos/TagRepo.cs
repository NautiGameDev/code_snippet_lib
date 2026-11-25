using CodeSnippetLib.Server.Data;
using CodeSnippetLib.Server.Interfaces.Repos;
using CodeSnippetLib.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippetLib.Server.Repos
{
    public class TagRepo : ITagRepo
    {
        private readonly ApplicationDbContext _context;

        public TagRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<List<Tag>> GetTagList(string[] tagArray)
        {
            List<string> normalizedTags = tagArray
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim().ToLowerInvariant())
                .Distinct()
                .ToList();

            if (!normalizedTags.Any()) return new List<Tag>();

            List<Tag> existingTags = await _context.Tags
                .Where(t => normalizedTags.Contains(t.Name))
                .ToListAsync();

            var existingNames = existingTags
                .Select(t => t.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            List<Tag> newTags = normalizedTags
                .Where(t => !existingNames.Contains(t))
                .Select(t => new Tag() { Name = t })
                .ToList();

            if (newTags.Any())
            {
                await _context.Tags.AddRangeAsync(newTags);
                await _context.SaveChangesAsync();
            }

            return existingTags.Concat(newTags).ToList();
        }

        public async Task MapTagsToPost(int postId, List<Tag> tags)
        {
            List<TagToPost> newDataList = tags.Select(t => new TagToPost() { PostId = postId, TagId = t.Id }).ToList();

            await _context.TagsToPost.AddRangeAsync(newDataList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagToPost(TagToPost ttp)
        {
            _context.TagsToPost.Remove(ttp);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTag(Tag tag)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }
}

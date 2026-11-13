using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Interfaces.Repos
{
    public interface ITagRepo
    {
        public Task<List<Tag>> GetTagList(string[] tagArray);
        public Task MapTagsToPost(int postId, List<Tag> tags);
        public Task<List<Tag>> GetAllTags();
        public Task DeleteTagToPost(TagToPost ttp);
        public Task DeleteTag(Tag tag);
    }
}

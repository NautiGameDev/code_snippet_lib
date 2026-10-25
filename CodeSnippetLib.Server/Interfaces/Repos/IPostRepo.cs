using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Interfaces.Repos
{
    public interface IPostRepo
    {
        public Task CreatePost(Post newPost);
        public Task<Post?> GetPostBySlug(string slug);
        public Task<List<TagToPost>> GetPostsByTagId(int tagId);

    }
}

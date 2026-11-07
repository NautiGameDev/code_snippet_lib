using CodeSnippetLib.Server.DTOs.Post;
using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Interfaces.Services
{
    public interface IPostService
    {
        public Task<(bool success, string message)> CreatePost(NewPostDto dto, string userId);
        public Task<Post?> GetPostBySlug(string slug);
        public Task<List<TagToPost>> GetPostsByTagId(int tagId);
        public Task<(List<PostDto> postDtos, int totalCount)> GetPostSelection(int skip, int take, string? search, string? tag);
        
    }
}

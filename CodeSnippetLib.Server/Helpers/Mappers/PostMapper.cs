using CodeSnippetLib.Server.DTOs.Post;
using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Helpers.Mappers
{
    public static class PostMapper
    {
        public static PostDto ToPostDto(this Post post)
        {
            return new PostDto()
            {
                Title = post.Title,
                Content = post.Content,
                Tags = post.TagToPosts.Any() ? post.TagToPosts.Select(ttp => ttp.Tag.Name).ToList() : new List<string>()
            };
        }
    }
}

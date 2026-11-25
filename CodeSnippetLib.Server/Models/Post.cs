using System.ComponentModel.DataAnnotations.Schema;
using CodeSnippetLib.Server.DTOs.Post;

namespace CodeSnippetLib.Server.Models
{
    [Table("Posts")]
    public class Post
    {
        public int Id { get; set; }
        public string? AuthorId { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public virtual List<TagToPost> TagToPosts { get; set; } = new List<TagToPost>();

        public void UpdatePost(UpdatePostDto dto)
        {
            Title = dto.Title;
            Slug = dto.Title.Replace(" ", "-");
            Content = dto.Content;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}

namespace CodeSnippetLib.Server.DTOs.Post
{
    public class PostDto
    {
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public string? Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}

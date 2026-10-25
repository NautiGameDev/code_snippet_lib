namespace CodeSnippetLib.Server.DTOs.Post
{
    public class PostDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}

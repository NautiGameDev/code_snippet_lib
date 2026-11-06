namespace CodeSnippetLib.Server.DTOs.Post
{
    public class GetPostsDto
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
    }
}

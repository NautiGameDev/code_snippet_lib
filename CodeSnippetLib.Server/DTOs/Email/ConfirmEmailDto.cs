namespace CodeSnippetLib.Server.DTOs.Email
{
    public class ConfirmEmailDto
    {
        public string? UserId { get; set; }
        public string? ConfirmationCode { get; set; }
    }
}

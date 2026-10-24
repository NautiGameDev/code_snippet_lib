using CodeSnippetLib.Server.DTOs.Email;

namespace CodeSnippetLib.Server.Interfaces.Services
{
    public interface IEmailService
    {
        public Task CreateEmail(NewEmailDto dto);
    }
}

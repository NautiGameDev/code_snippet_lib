using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Interfaces.Services
{
    public interface ITokenService
    {
        public Task<string> CreateToken(Account account);
        public string CreateRefreshToken();
    }
}

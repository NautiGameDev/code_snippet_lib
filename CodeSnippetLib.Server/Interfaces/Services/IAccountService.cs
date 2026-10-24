using CodeSnippetLib.Server.DTOs.Account;
using CodeSnippetLib.Server.DTOs.API;
using CodeSnippetLib.Server.DTOs.Email;
using CodeSnippetLib.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace CodeSnippetLib.Server.Interfaces.Services
{
    public interface IAccountService
    {
        public Task<AuthResultDto> Login(LoginDto dto, string ip);
        public Task<AuthResultDto> Register(NewAccountDto dto, string ip);
        public Task<AuthResultDto> UpdateAccount(string userId, UpdateAccountDto dto);
        public Task<AuthResultDto> RefreshToken(string rawRefreshToken, string ip);
        public Task RevokeToken(string refreshToken);
        public Task<AuthResultDto> ConfirmAccount(ConfirmEmailDto dto);
        public Task<AuthResultDto> RequestPasswordReset(string userEmail);
        public Task<AuthResultDto> ConfirmPasswordReset(PasswordResetDto dto);
    }
}

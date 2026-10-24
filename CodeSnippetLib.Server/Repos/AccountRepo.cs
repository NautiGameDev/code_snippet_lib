using System.Security.Cryptography;
using System.Text;
using CodeSnippetLib.Server.Data;
using CodeSnippetLib.Server.Helpers;
using CodeSnippetLib.Server.Interfaces.Repos;
using CodeSnippetLib.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippetLib.Server.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly ApplicationDbContext _context;

        public AccountRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateRefreshToken(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RefreshToken>> GetAllRefreshTokensByUser(string userId)
        {
            return await _context.RefreshTokens.Where(t => t.UserId == userId)
                .OrderByDescending(x => x.Created)
                .ToListAsync();
        }

        public async Task<RefreshToken?> GetTokenByRawToken(string rawRefreshToken)
        {
            string tokenHash = TokenHasher.HashToken(rawRefreshToken);

            return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
        }        

        public async Task UpdateRefreshToken(RefreshToken token)
        {
            await _context.SaveChangesAsync();
        }
    }
}

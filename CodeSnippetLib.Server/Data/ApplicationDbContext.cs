using CodeSnippetLib.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeSnippetLib.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<Account>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagToPost> TagsToPost { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "6a83e9d4-a03f-4692-8f6b-6cdf372da900",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "6a83e9d4-a03f-4692-8f6b-6cdf372da900"
                },
                new IdentityRole
                {
                    Id = "51f82c97-851b-4558-87ac-52fc0dacb5d5",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "51f82c97-851b-4558-87ac-52fc0dacb5d5"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Interfaces.Services
{
    public interface ITagService
    {
        public Task<List<Tag>> GetAllTags();
    }
}

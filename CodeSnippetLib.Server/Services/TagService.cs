using CodeSnippetLib.Server.Interfaces.Repos;
using CodeSnippetLib.Server.Interfaces.Services;
using CodeSnippetLib.Server.Models;

namespace CodeSnippetLib.Server.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepo _tagRepo;

        public TagService(ITagRepo tagRepo)
        {
            _tagRepo = tagRepo;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _tagRepo.GetAllTags();
        }
    }
}

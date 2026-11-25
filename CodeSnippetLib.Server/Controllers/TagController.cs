using CodeSnippetLib.Server.Interfaces.Services;
using CodeSnippetLib.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeSnippetLib.Server.Controllers
{
    public class TagController : AppBaseController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            List<Tag> tags = await _tagService.GetAllTags();

            return Ok(new { message = "Tags retrieved", tags = tags });
        }

    }
}

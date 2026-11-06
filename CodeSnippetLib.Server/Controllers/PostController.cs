using CodeSnippetLib.Server.DTOs.Post;
using CodeSnippetLib.Server.Helpers.Mappers;
using CodeSnippetLib.Server.Interfaces.Services;
using CodeSnippetLib.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeSnippetLib.Server.Controllers
{
    public class PostController : AppBaseController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewPost([FromBody] NewPostDto dto)
        {
            if (string.IsNullOrEmpty(UserId)) return BadRequest(new { message = "User ID not found in token" });

            var (success, message) = await _postService.CreatePost(dto, UserId);

            if (!success) return BadRequest(new { message = message });

            return StatusCode(StatusCodes.Status201Created, new { message = message });
        }

        [HttpGet("by-slug/{slug}")]
        public async Task<IActionResult> GetPostBySlug(string slug)
        {
            Post? post = await _postService.GetPostBySlug(slug);

            if (post == null)
            {
                return NotFound(new { message = "Post could not be found" });
            }

            return Ok(post.ToPostDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetPostSelection([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            List<PostDto> posts = await _postService.GetPostSelection(skip, take);

            return Ok(new { message = "Posts retrieved successfully", posts = posts });
        }

        [HttpGet("get-count")]
        public async Task<IActionResult> GetPostCount()
        {
            int count = await _postService.GetPostCount();

            return Ok(new { message = "Count retrieved successfully", count = count });
        }
    }
}

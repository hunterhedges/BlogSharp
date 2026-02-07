using BlogSharp.Shared.Interfaces;
using BlogSharp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogSharp.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogPostController : ControllerBase
{
    private readonly IBlogPostService _blogService;

    public BlogPostController(IBlogPostService blogService)
    {
        _blogService = blogService;
    }

    // GET: api/blogpost
    [HttpGet]
    public async Task<ActionResult<List<BlogPost>>> GetPosts()
    {
        var posts = await _blogService.GetPostsAsync();
        return Ok(posts);
    }

    // GET: api/blogpost/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BlogPost>> GetPost(Guid id)
    {
        var post = await _blogService.GetPostByIdAsync(id);

        if (post == null)
            return NotFound();

        return Ok(post);
    }

    // POST: api/blogpost
    [HttpPost]
    public async Task<ActionResult<BlogPost>> CreatePost(BlogPost post)
    {
        // Ensure we aren't trying to create a post with an existing ID
        // The service or EntityBase will handle generating a new one
        var createdPost = await _blogService.CreatePostAsync(post);

        if (createdPost == null)
            return BadRequest("Could not create the post.");

        return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
    }

    // PUT: api/blogpost/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePost(Guid id, BlogPost post)
    {
        if (id != post.Id)
            return BadRequest("ID mismatch");

        var updatedPost = await _blogService.UpdatePostAsync(post);

        if (updatedPost == null)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/blogpost/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePost(Guid id)
    {
        var success = await _blogService.DeletePostAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
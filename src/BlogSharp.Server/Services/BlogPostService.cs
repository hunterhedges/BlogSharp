using BlogSharp.Server.Data;
using BlogSharp.Shared.Interfaces;
using BlogSharp.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlogSharp.Server.Services;

public class BlogPostService : IBlogPostService
{
    private readonly IDbContextFactory<BlogSharpDbContext> _factory;

    public BlogPostService(IDbContextFactory<BlogSharpDbContext> factory)
    {
        _factory = factory;
    }

    // 1. Get All Posts (Materialized List)
    public async Task<List<BlogPost>> GetPostsAsync()
    {
        using var context = _factory.CreateDbContext();
        return await context.BlogPosts
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    // 2. Stream Posts (For large datasets/UX)
    public async IAsyncEnumerable<BlogPost> StreamPostsAsync()
    {
        using var context = _factory.CreateDbContext();
        var posts = context.BlogPosts
            .OrderByDescending(p => p.CreatedAt)
            .AsNoTracking()
            .AsAsyncEnumerable();

        await foreach (var post in posts)
        {
            yield return post;
        }
    }

    // 3. Get Single Post with Comments
    public async Task<BlogPost?> GetPostByIdAsync(Guid id)
    {
        using var context = _factory.CreateDbContext();
        return await context.BlogPosts
            .Include(p => p.Comments) // Eager load the comments
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // 4. Create Post
    public async Task<BlogPost?> CreatePostAsync(BlogPost post)
    {
        using var context = _factory.CreateDbContext();
        context.BlogPosts.Add(post);
        await context.SaveChangesAsync();
        return post;
    }

    // 5. Update Post
    public async Task<BlogPost?> UpdatePostAsync(BlogPost post)
    {
        using var context = _factory.CreateDbContext();

        // Attach the post and mark as modified
        context.Entry(post).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
            return post;
        }
        catch (DbUpdateConcurrencyException)
        {
            return null; // Handle concurrency if the post was deleted/changed
        }
    }

    // 6. Delete Post
    public async Task<bool> DeletePostAsync(Guid id)
    {
        using var context = _factory.CreateDbContext();
        var post = await context.BlogPosts.FindAsync(id);

        if (post == null) return false;

        context.BlogPosts.Remove(post);
        await context.SaveChangesAsync();
        return true;
    }
}

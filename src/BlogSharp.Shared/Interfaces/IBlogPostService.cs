using BlogSharp.Shared.Models;

namespace BlogSharp.Shared.Interfaces;

public interface IBlogPostService
{
    Task<List<BlogPost>> GetPostsAsync();
    IAsyncEnumerable<BlogPost> StreamPostsAsync();
    Task<BlogPost?> GetPostByIdAsync(Guid id);
    Task<BlogPost?> CreatePostAsync(BlogPost post);
    Task<BlogPost?> UpdatePostAsync(BlogPost post);
    Task<bool> DeletePostAsync(Guid id);
}
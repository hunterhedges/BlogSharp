using BlogSharp.Shared.Interfaces;
using BlogSharp.Shared.Models;
using System.Net.Http.Json;

namespace BlogSharp.Client.Services;

public class ClientBlogPostService : IBlogPostService
{
    private readonly HttpClient _http;

    public ClientBlogPostService(HttpClient http)
    {
        _http = http;
    }

    // 1. Get All Posts
    public async Task<List<BlogPost>> GetPostsAsync()
    {
        return await _http.GetFromJsonAsync<List<BlogPost>>("api/blogpost") ?? new();
    }

    // 2. Stream Posts (Client-side consumption)
    public async IAsyncEnumerable<BlogPost> StreamPostsAsync()
    {
        // GetFromJsonAsAsyncEnumerable handles the JSON stream parsing automatically
        var stream = _http.GetFromJsonAsAsyncEnumerable<BlogPost>("api/blogpost/stream");

        if (stream == null) yield break;

        await foreach (var post in stream)
        {
            if (post != null) yield return post;
        }
    }

    // 3. Get Single Post by ID
    public async Task<BlogPost?> GetPostByIdAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<BlogPost>($"api/blogpost/{id}");
    }

    // 4. Create Post
    public async Task<BlogPost?> CreatePostAsync(BlogPost post)
    {
        var response = await _http.PostAsJsonAsync("api/blogpost", post);
        return await response.Content.ReadFromJsonAsync<BlogPost>();
    }

    // 5. Update Post
    public async Task<BlogPost?> UpdatePostAsync(BlogPost post)
    {
        var response = await _http.PutAsJsonAsync($"api/blogpost/{post.Id}", post);

        if (response.IsSuccessStatusCode)
        {
            return post;
        }
        return null;
    }

    // 6. Delete Post
    public async Task<bool> DeletePostAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"api/blogpost/{id}");
        return response.IsSuccessStatusCode;
    }
}
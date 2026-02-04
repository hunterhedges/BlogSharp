using Microsoft.AspNetCore.Components;

namespace BlogSharp.Components.Pages;

public partial class Home : ComponentBase
{
    private List<PostDto>? posts;
    private bool isLoading = true;

    // Lightweight DTO used by this page so the page does not depend on any particular service type.
    private sealed record PostDto(Guid Id, string Title, string Content, DateTime CreatedAt, int CommentsCount);

    protected override async Task OnInitializedAsync()
    {
        try
        {
            // Try to load posts from your API endpoint. Adjust the path if your API uses a different route.
            var result = await Http.GetFromJsonAsync<List<PostDto>>("api/posts");
            posts = result ?? new List<PostDto>();
        }
        catch
        {
            // If no API/service exists yet, provide a small local sample so the page is usable immediately.
            posts = new List<PostDto>
            {
                new PostDto(Guid.NewGuid(), "Welcome to BlogSharp", "This is the first post. Replace this sample by implementing an API or service that returns posts at /api/posts.", DateTime.UtcNow, 0),
                new PostDto(Guid.NewGuid(), "Getting started with BlogSharp", "Create posts, add comments, and customize your layout. This placeholder shows how posts will appear.", DateTime.UtcNow.AddDays(-2), 2)
            };
        }
        finally
        {
            isLoading = false;
        }
    }
}
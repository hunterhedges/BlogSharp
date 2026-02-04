using BlogSharp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlogSharp.Components.Pages;

public partial class Post : ComponentBase
{
    [Parameter]
    public Guid Id { get; set; }

    [Inject]
    public BlogSharpDbContext Db { get; set; } = null!;

    // Use fully-qualified data type to avoid name ambiguity with this component class.
    private BlogSharp.Data.Post? post;
    private bool isLoading;

    protected override async Task OnParametersSetAsync()
    {
        isLoading = true;

        try
        {
            post = await Db.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }
        finally
        {
            isLoading = false;
        }
    }
}
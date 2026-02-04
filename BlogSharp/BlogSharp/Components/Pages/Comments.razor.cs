using Microsoft.AspNetCore.Components;

namespace BlogSharp.Components.Pages;

public partial class Comments : ComponentBase
{
    [Parameter]
    public Data.Post Post { get; set; } = null!;
}

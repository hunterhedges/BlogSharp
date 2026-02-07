namespace BlogSharp.Shared.Models;

public class BlogComment : EntityBase
{
    public string Text { get; set; } = string.Empty;
    public Guid PostId { get; set; }
    public BlogPost Post { get; set; } = null!;
}
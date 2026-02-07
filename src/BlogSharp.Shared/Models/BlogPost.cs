namespace BlogSharp.Shared.Models;

public class BlogPost : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<BlogComment> Comments { get; set; } = new();
}
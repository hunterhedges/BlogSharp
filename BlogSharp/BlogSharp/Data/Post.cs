namespace BlogSharp.Data;

public class Post : TransactionEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public List<Comment> Comments { get; set; } = new();
}
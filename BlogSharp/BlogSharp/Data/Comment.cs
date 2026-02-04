namespace BlogSharp.Data;

public class Comment : TransactionEntity
{
    public string Text { get; set; } = string.Empty;
    public Guid PostId { get; set; }    
    public Post Post { get; set; } = null!;
}

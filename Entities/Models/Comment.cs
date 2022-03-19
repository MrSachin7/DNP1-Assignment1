namespace Entities.Models; 

public class Comment {
    public string Body { get; set; }
    public User Writer { get; set; }
    public Comment? ParentComment { get; set; }
    public DateTime CreatedAt { get; set; }
}
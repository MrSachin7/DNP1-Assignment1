namespace Entities.Models;

public class Post {
    public string Header { get; set; }
    public string Body { get; set; }
    public User WrittenBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Id { get; set; }

    public Post() {
    }
}
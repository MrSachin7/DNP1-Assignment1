using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Post {
    public string Header { get; set; }
    public string Body { get; set; }
    public User WrittenBy { get; set; }
    public DateTime CreatedAt { get; set; }

 [Key]   public int Id { get; set; }

    public ICollection<Comment> Comments { get; set; }


    public Post() {
        Comments = new List<Comment>();
    }
}
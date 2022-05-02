using System.ComponentModel.DataAnnotations;

namespace Entities.Models; 

public class Comment {
    public string Body { get; set; }
    public User Writer { get; set; }
    public DateTime CreatedAt { get; set; }

    [Key] public int Id { get; set; }
}
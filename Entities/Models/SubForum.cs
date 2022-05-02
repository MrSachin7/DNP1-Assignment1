using System.ComponentModel.DataAnnotations;

namespace Entities.Models; 

public class SubForum {
    public User OwnedBy { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime CreatedAt { get;  set; }

    public int Views { get; set; }


    [Key]
    public int Id { get; set; }

    public ICollection<Post> AllPosts { get; set; }



    public SubForum() {
        AllPosts = new List<Post>();
        // CreatedAt = DateTime.Now;
    }
}
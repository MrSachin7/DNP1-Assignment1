namespace Entities.Models; 

public class SubForum {
    public User OwnedBy { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime CreatedAt { get;  set; }

    public int Views { get; set; }

    public int Id { get; set; }

    public ICollection<Post> AllPosts { get; set; }


    public SubForum(User OwnedBy, string Title, string Description) {
        this.OwnedBy = OwnedBy;
        this.Title = Title;
        this.Description = Description;
        AllPosts = new List<Post>();
        CreatedAt= DateTime.Now;
        Views = 0;
    }

    public SubForum() {
        AllPosts = new List<Post>();
        CreatedAt = DateTime.Now;
    }
}
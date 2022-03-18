namespace Entities.Models;

public class Forum {
    public ICollection<SubForum> AllSubForums { get; set; }
    public string ForumName { get; set; }
    public string ForumDescription { get; set; }
    public int Id { get; set; }

    public int Views {
        get;
        set;
    }

    public Forum(string ForumName, string ForumDescritption) {
        this.ForumName = ForumName;
        this.ForumDescription = ForumDescritption;
        AllSubForums = new List<SubForum>();
        Views = 0;

    }

    public Forum() {
        AllSubForums = new List<SubForum>();

    }
}
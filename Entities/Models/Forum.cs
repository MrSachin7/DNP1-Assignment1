﻿namespace Entities.Models;

public class Forum {
    public ICollection<SubForum> AllSubForums { get; set; }
    public string ForumName { get; set; }
    public string ForumDescription { get; set; }
    public int Id { get; set; }

    public int Views {
        get;
        set;
    }


    public Forum() {
        AllSubForums = new List<SubForum>();

    }
}
namespace Entities.Models; 

public class Post {
     public string Header { get; set; }
     public string Body { get; set; }
     public User WrittenBy { get; set; }

     public Post(string Header, string Body, User WrittenBy) {
          this.Header = Header;
          this.Body = Body;
          this.WrittenBy = WrittenBy;
     }
}
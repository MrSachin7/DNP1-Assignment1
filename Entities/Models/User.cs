using System.ComponentModel.DataAnnotations;

namespace Entities.Models; 

public class User {
    [Key]
    public string Username { get; set; }
    public  string? Password { get; set; }

    public User(string Username, string Password) {
        this.Username = Username;
        this.Password = Password;
    }

    public User(string Username) {
        this.Username = Username;
        Password = null;
    }

    public User() {

    }
}
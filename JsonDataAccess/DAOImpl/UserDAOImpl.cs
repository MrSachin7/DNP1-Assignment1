using Application;
using Entities;
using Entities.Models;

namespace JsonDataAccess;

public class UserDAOImpl : IUserDAO {
    private JsonUserContext fileContext;

    public UserDAOImpl(JsonUserContext fileContext) {
        this.fileContext = fileContext;
    }

    public async Task createUserAsync(User user) {
        fileContext.Users.Add(user);
        await fileContext.SaveChangesAsync();
    }

    public async Task<bool> doesUsernameExist(string username) {
        ICollection<User> allUsers = fileContext.Users;
        return allUsers.Any(user => user.Username.Equals(username));
    }

    public Task<User> GetUserAsync(string username) {
        List<User> users = fileContext.Users.ToList();
        User? find= users.Find(user => user.Username.Equals(username));
        if (find==null) {
            throw new Exception("Username doesnt exist");
        }
        return Task.FromResult(find);
    }
}
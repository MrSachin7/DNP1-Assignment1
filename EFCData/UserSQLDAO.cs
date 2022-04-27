using Application;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCData; 

public class UserSQLDAO  : IUserDAO{
    private readonly ForumContext forumContext;

    public UserSQLDAO(ForumContext forumContext) {
        this.forumContext = forumContext;
    }


    public async Task createUserAsync(User user) {
        await forumContext.Users.AddAsync(user);
       await forumContext.SaveChangesAsync();
    }

    public async Task<bool> doesUsernameExist(string username) {
        bool boolean = forumContext.Users.Any(user => user.Username.Equals(username));
        return boolean;
    }

    public async Task<User> GetUserAsync(string username) {
        User first =await forumContext.Users.FirstAsync(user => user.Username.Equals(username));
        return first;
    }
}
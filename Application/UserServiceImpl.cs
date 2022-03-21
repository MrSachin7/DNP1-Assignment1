using System.Security.Claims;
using System.Text.Json;
using Contracts;
using Entities;
using Entities.Models;

namespace Application;

public class UserServiceImpl : IUserService {
    private readonly IUserDAO userDao;

    public UserServiceImpl(IUserDAO userDao) {
        this.userDao = userDao;
    }


    public async Task CreateUserAsync(string username, string password) {
        validateUsername(username);
        validatePassword(password);

        if (await userDao.doesUsernameExist(username)) {
            // TODO ASK troels if the await is appropriate
            throw new Exception($"Username, {username} already exists. Please choose another");
        }

        await userDao.createUserAsync(new User(username,
            password)); // TODO ask troels if async and await is properly placed...
    }

    public async Task<User> GetUserAsync(string username) {
        User? find = await userDao.GetUserAsync(username);
        return find;
    }


    private void validatePassword(string password) {
        if (string.IsNullOrEmpty(password)) {
            throw
                new Exception(
                    "Password cannot be empty"); //TODO Ask troels if these throwing exception is a good/bad idea
        }

        if (password.Length <= 5) {
            throw new Exception("Password must be greater than five characters due to security reasons");
        }

        int count = 0;
        foreach (char c in password) {
            if (Char.IsNumber(c)) {
                count++;
                break;
            }
        }

        if (count == 0) {
            throw new Exception("Password must have at least one digit for security reasons");
        }
    }

    private void validateUsername(string username) {
        if (string.IsNullOrEmpty(username)) {
            throw new Exception("Username cannot be empty");
        }

        if (username.Length <= 5) {
            throw new Exception("Username must be greater than five characters");
        }
    }
}
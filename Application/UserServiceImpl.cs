using System.Security.Claims;
using System.Text.Json;
using Contracts;
using Entities;
using Entities.Models;

namespace Application;

public class UserServiceImpl : IUserService {
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    private readonly IUserDAO userDao;
  //  private readonly IJSRuntime jsRunTime;


    private ClaimsPrincipal? principal;

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

    public async Task LoginAsync(string username, string password) {
        User user = await userDao.GetUserAsync(username);
        ValidateLogincredentials(password, user);
        await CacheUserAsync(user);
        principal = CreateClaimsPrincipal(user);
        OnAuthStateChanged?.Invoke(principal);
    }

    private ClaimsPrincipal? CreateClaimsPrincipal(User user) {
        ClaimsIdentity identity = new ClaimsIdentity();
        if (user!=null) {
            identity = ConvertUserToClaimsIdentity(user);
        }

        ClaimsPrincipal tempPrincipal = new ClaimsPrincipal(identity);
        return tempPrincipal;
    }

    private ClaimsIdentity ConvertUserToClaimsIdentity(User user) {
        List<Claim> claims = new List<Claim>() {
            new Claim(ClaimTypes.Name, user.Username)
        };
        return new ClaimsIdentity(claims, "apiauth_type");
    }

    private async Task CacheUserAsync(User user) {
        string serializedData = JsonSerializer.Serialize(user);
      //  await jsRunTime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedData);
       // TODO ask troels how to manage jsRuntime invocation from outside the blazor project...
    }

    private void ValidateLogincredentials(string password, User user) {
        if (user==null) {
            throw new Exception("Username not found");
        }

        if (!password.Equals(user.Password)) {
            throw new Exception("Password Incorrect");
        }
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
        else if (username.Length <= 5) {
            throw new Exception("Username must be greater than five characters");
        }
    }
}
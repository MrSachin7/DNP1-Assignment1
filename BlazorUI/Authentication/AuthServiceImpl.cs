using System.Security.Claims;
using System.Text.Json;
using Contracts;
using Entities.Models;
using Microsoft.JSInterop;

namespace BlazorLogin.Authentication;

public class AuthServiceImpl : IAuthService {
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    private readonly IUserService userService;
    private readonly IJSRuntime jsRunTime;
    private ClaimsPrincipal? principal;

    public AuthServiceImpl(IUserService userService, IJSRuntime jsRunTime) {
        this.userService = userService;
        this.jsRunTime = jsRunTime;      
        principal = null;
    }

    public async Task LoginAsync(string username, string password) {
        User user = await userService.GetUserAsync(username);
        ValidateLoginCredentials(password, user);
        await CacheUserAsync(user);
        principal = CreateClaimsPrincipal(user);
        OnAuthStateChanged?.Invoke(principal);
    }

    private ClaimsPrincipal CreateClaimsPrincipal(User? user) {
        ClaimsIdentity identity = new();
        if (user != null) {
            identity = ConvertUserToClaimsIdentity(user);
        }

        ClaimsPrincipal tempPrincipal = new ClaimsPrincipal(identity);
        return tempPrincipal;
    }

    private ClaimsIdentity ConvertUserToClaimsIdentity(User user) {
        List<Claim> claims = new List<Claim>() {
            new Claim(ClaimTypes.Name, user.Username),
            
        };
        return new ClaimsIdentity(claims, "apiauth_type");
    }

    private async Task CacheUserAsync(User? user) {
        string serializedData = JsonSerializer.Serialize(user);
        await jsRunTime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedData);
    }

    private void ValidateLoginCredentials(string password, User user) {
        // if (user == null) {
        //     throw new Exception("Username not found");
        // }

        if (!password.Equals(user.Password)) {
            throw new Exception("Password Incorrect");
        }
    }

    public async Task LogOutAsync() {
        await ClearUserFromCacheAsync();
        ClaimsPrincipal principal = CreateClaimsPrincipal(null);
        OnAuthStateChanged?.Invoke(principal);
    }

    private async Task ClearUserFromCacheAsync() {
        await jsRunTime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }

    public async Task<ClaimsPrincipal> GetAuthAsync() {
        if (principal != null) {
            return principal;
        }

        string userAsJson = await jsRunTime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (string.IsNullOrEmpty(userAsJson)) {
            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        User? user = JsonSerializer.Deserialize<User>(userAsJson);
        user = await userService.GetUserAsync(user.Username);
        principal = CreateClaimsPrincipal(user);
        return principal;
    }
}
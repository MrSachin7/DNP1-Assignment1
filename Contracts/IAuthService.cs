using System.Security.Claims;

namespace Contracts;

public interface IAuthService {
    Task LoginAsync(string username, string password);
    public Task LogOutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}
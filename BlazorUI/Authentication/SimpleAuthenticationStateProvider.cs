using System.Security.Claims;
using Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUI.Authentication; 

public class SimpleAuthenticationStateProvider: AuthenticationStateProvider {

    private readonly IAuthService authService;

    public SimpleAuthenticationStateProvider(IAuthService authService) {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }

    private void AuthStateChanged(ClaimsPrincipal principal) {
     NotifyAuthenticationStateChanged(
         Task.FromResult(new AuthenticationState(principal)));
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        ClaimsPrincipal principal = await authService.GetAuthAsync();
        return await Task.FromResult(new AuthenticationState(principal));
    }
}
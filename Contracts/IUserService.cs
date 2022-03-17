

using System.Security.Claims;
using Entities.Models;

namespace Contracts; 

public interface IUserService {
    Task CreateUserAsync(string username, string password);
    Task<User> GetUserAsync(string userUsername);
}
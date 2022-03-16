

namespace Contracts; 

public interface IUserService {
    Task CreateUserAsync(string username, string password);
    Task LoginAsync(string username, string password);
}
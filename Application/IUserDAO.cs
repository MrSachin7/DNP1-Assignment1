
using Entities.Models;

namespace Application;

public interface IUserDAO {
    Task createUserAsync(User user);

    //   Task<bool>
    Task<bool> doesUsernameExist(string username);   // TODO ask troels if there should be task and if yes, how to manage the used condition??
    Task<User> GetUserAsync(string username);
}
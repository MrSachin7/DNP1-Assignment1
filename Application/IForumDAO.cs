using Entities.Models;

namespace Application; 

public interface IForumDAO {
    Task<List<Forum>> GetAllForums();

    Task AddForumAsync(Forum newForumItem);
    Task<Forum> GetForumByIdAsync(int id);
}
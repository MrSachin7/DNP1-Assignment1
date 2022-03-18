using Entities.Models;

namespace Application; 

public interface IForumDAO {
    Task AddForumAsync(Forum newForumItem);
    Task<Forum> GetForumByIdAsync(int id);
    Task<List<Forum>> GetAllForumsAsync();
    Task<SubForum?> GetSubForumAsync(int forumId, int subForumId);
    Task AddSubForumAsync(SubForum newSubForumItem, int forumId);
}
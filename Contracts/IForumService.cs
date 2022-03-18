using Entities.Models;

namespace Contracts; 

public interface IForumService {
    // Task<List<Post>> GetAllPostsAsync();
    Task AddForumAsync(Forum newForumItem);
    Task<Forum> GetForumByIdAsync(int id);
    Task<List<Forum>> GetAllForumsAsync();
    Task<SubForum?> GetSubForumAsync(int forumId, int subForumId);
    Task AddSubForumAsync(SubForum newSubForumItem, int forumId);
}
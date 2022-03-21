using Entities.Models;

namespace Application; 

public interface IForumDAO {
    Task AddForumAsync(Forum newForumItem);
    Task<Forum> GetForumByIdAsync(int id);
    Task<List<Forum>> GetAllForumsAsync();
    Task<SubForum?> GetSubForumAsync(int forumId, int subForumId);
    Task AddSubForumAsync(SubForum newSubForumItem, int forumId);
    Task IncrementViewOfForumAsync(int forumId);
    Task AddPostAsync(Post newPostItem, int forumId, int subForumId);
    Task IncrementViewOfSubForumAsync(int forumId, int subForumId);
    Task<Post?> GetPostAsync(int forumId, int subForumId, int postId);

    Task<Comment> AddCommentToPost(int forumId, int subForumId, int postId, Comment commentToPost);
}
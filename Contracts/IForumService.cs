using Entities.Models;

namespace Contracts; 

public interface IForumService {
    // Task<List<Post>> GetAllPostsAsync();
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

    Task<Comment> EditComment(int forumId, int subForumId, int postId, Comment editedComment);
    Task<Comment> DeleteComment(int forumId, int subForumId, int postId, Comment comment);
}
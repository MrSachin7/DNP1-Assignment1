using Entities.Models;

namespace Application; 

public interface IForumDAO {
    Task<Forum> AddForumAsync(Forum newForumItem);
    Task<Forum> GetForumByIdAsync(int id);
    Task<List<Forum>> GetAllForumsAsync();
    Task<SubForum?> GetSubForumAsync(int forumId, int subForumId);
    Task<SubForum> AddSubForumAsync(SubForum newSubForumItem, int forumId);
    Task IncrementViewOfForumAsync(int forumId);
    Task<Post> AddPostAsync(Post newPostItem, int forumId, int subForumId);
    Task IncrementViewOfSubForumAsync(int forumId, int subForumId);
    Task<Post?> GetPostAsync(int forumId, int subForumId, int postId);

    Task<Comment> AddCommentToPost(int forumId, int subForumId, int postId, Comment commentToPost);
    Task<Comment> EditComment(int forumId, int subForumId, int postId, Comment editedComment);
    Task<Comment> DeleteComment(int forumId, int subForumId, int postId, int commentId);
}
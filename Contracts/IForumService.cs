using Entities.Models;

namespace Contracts; 

public interface IForumService {
    // Task<List<Post>> GetAllPostsAsync();
    Task<Forum> AddForumAsync(Forum newForumItem);
    Task<Forum> GetForumByIdAsync(int id);
    Task<List<Forum>> GetAllForumsAsync();
    Task<SubForum?> GetSubForumAsync( int subForumId);
    Task<SubForum> AddSubForumAsync(SubForum newSubForumItem, int forumId);
    Task IncrementViewOfForumAsync(int forumId);
    Task<Post> AddPostAsync(Post newPostItem, int subForumId);
    Task IncrementViewOfSubForumAsync( int subForumId);
    Task<Post?> GetPostAsync( int postId);
    Task<Comment> AddCommentToPost(int postId, Comment commentToPost);

    Task<Comment> EditComment(Comment editedComment);
    Task<Comment> DeleteComment( int commentId);
}
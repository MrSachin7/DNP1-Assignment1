using Contracts;
using Entities.Models;

namespace HttpServices;

public class ForumHttpClient : IForumService{
    public Task<Forum> AddForumAsync(Forum newForumItem) {
        throw new NotImplementedException();
    }

    public Task<Forum> GetForumByIdAsync(int id) {
        throw new NotImplementedException();
    }

    public Task<List<Forum>> GetAllForumsAsync() {
        throw new NotImplementedException();
    }

    public Task<SubForum?> GetSubForumAsync(int forumId, int subForumId) {
        throw new NotImplementedException();
    }

    public Task<SubForum> AddSubForumAsync(SubForum newSubForumItem, int forumId) {
        throw new NotImplementedException();
    }

    public Task IncrementViewOfForumAsync(int forumId) {
        throw new NotImplementedException();
    }

    public Task<Post> AddPostAsync(Post newPostItem, int forumId, int subForumId) {
        throw new NotImplementedException();
    }

    public Task IncrementViewOfSubForumAsync(int forumId, int subForumId) {
        throw new NotImplementedException();
    }

    public Task<Post?> GetPostAsync(int forumId, int subForumId, int postId) {
        throw new NotImplementedException();
    }

    public Task<Comment> AddCommentToPost(int forumId, int subForumId, int postId, Comment commentToPost) {
        throw new NotImplementedException();
    }

    public Task<Comment> EditComment(int forumId, int subForumId, int postId, Comment editedComment) {
        throw new NotImplementedException();
    }

    public Task<Comment> DeleteComment(int forumId, int subForumId, int postId, int commentId) {
        throw new NotImplementedException();
    }
}
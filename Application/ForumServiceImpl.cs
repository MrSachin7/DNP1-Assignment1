using Contracts;
using Entities.Models;

namespace Application;

public class ForumServiceImpl : IForumService {
    private IForumDAO forumDAO;

    public ForumServiceImpl(IForumDAO forumDao) {
        forumDAO = forumDao;
    }


    // public Task<List<Post>> GetAllPostsAsync() {
    //     forumDAO.
    // }
    public async Task<Forum> AddForumAsync(Forum newForumItem) {
        //  Console.WriteLine("ForumService");
      return await forumDAO.AddForumAsync(newForumItem);
    }

    public async Task<Forum> GetForumByIdAsync(int id) {
        return await forumDAO.GetForumByIdAsync(id);
    }

    public async Task<List<Forum>> GetAllForumsAsync() {
        return await forumDAO.GetAllForumsAsync();
    }

    public async Task<SubForum?> GetSubForumAsync(int forumId, int subForumId) {
        return await forumDAO.GetSubForumAsync(forumId, subForumId);
    }

    public async Task<SubForum> AddSubForumAsync(SubForum newSubForumItem, int forumId) {
       return  await forumDAO.AddSubForumAsync(newSubForumItem, forumId);
    }

    public async Task IncrementViewOfForumAsync(int forumId) {
        await forumDAO.IncrementViewOfForumAsync(forumId);
    }

    public async Task<Post> AddPostAsync(Post newPostItem, int forumId, int subForumId) {
      return  await forumDAO.AddPostAsync(newPostItem, forumId, subForumId);
    }

    public async Task IncrementViewOfSubForumAsync(int forumId, int subForumId) {
        await forumDAO.IncrementViewOfSubForumAsync(forumId, subForumId);
    }

    public async Task<Post?> GetPostAsync(int forumId, int subForumId, int postId) {
        return await forumDAO.GetPostAsync(forumId, subForumId, postId);

    }

    public async Task<Comment> AddCommentToPost(int forumId, int subForumId, int postId, Comment commentToPost) {
       return await forumDAO.AddCommentToPost(forumId, subForumId, postId, commentToPost);
    }

    public async Task<Comment> EditComment(int forumId, int subForumId, int postId, Comment editedComment) {
        return await forumDAO.EditComment(forumId, subForumId, postId, editedComment);
        
    }

    public async Task<Comment> DeleteComment(int forumId, int subForumId, int postId, int commentId) {
        return await forumDAO.DeleteComment(forumId, subForumId, postId, commentId);
    }

    public void IncrementViewOfForumAsync() {
        throw new NotImplementedException();
    }
}
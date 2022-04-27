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

    public async Task<SubForum?> GetSubForumAsync( int subForumId) {
        return await forumDAO.GetSubForumAsync(subForumId);
    }

    public async Task<SubForum> AddSubForumAsync(SubForum newSubForumItem, int forumId) {
        SubForum subForum = await forumDAO.AddSubForumAsync(newSubForumItem, forumId);
        return subForum;
    }

    public async Task IncrementViewOfForumAsync(int forumId) {
        await forumDAO.IncrementViewOfForumAsync(forumId);
    }

    public async Task<Post> AddPostAsync(Post newPostItem,int subForumId) {
      return  await forumDAO.AddPostAsync(newPostItem, subForumId);
    }

    public async Task IncrementViewOfSubForumAsync(int subForumId) {
        await forumDAO.IncrementViewOfSubForumAsync( subForumId);
    }

    public async Task<Post?> GetPostAsync( int postId) {
        return await forumDAO.GetPostAsync( postId);

    }

    public async Task<Comment> AddCommentToPost( int postId, Comment commentToPost) {
       return await forumDAO.AddCommentToPost( postId, commentToPost);
    }

    public async Task<Comment> EditComment( Comment editedComment) {
        return await forumDAO.EditComment( editedComment);
        
    }

    public async Task<Comment> DeleteComment( int commentId) {
        return await forumDAO.DeleteComment( commentId);
    }

    public void IncrementViewOfForumAsync() {
        throw new NotImplementedException();
    }
}
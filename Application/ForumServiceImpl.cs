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
    public async Task AddForumAsync(Forum newForumItem) {
      //  Console.WriteLine("ForumService");
       await forumDAO.AddForumAsync(newForumItem);
       
    }
}
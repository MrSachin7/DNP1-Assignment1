using Application;
using Entities.Models;

namespace JsonDataAccess;

public class ForumDAOImpl : IForumDAO {
    private JsonForumContext fileContext;

    public async Task<List<Forum>> GetAllForums() {
        return fileContext.Forums.ToList();
    }

    public async Task AddForumAsync(Forum newForumItem) {
        int largestId = fileContext.Forums.Max(forum => forum.Id);
        newForumItem.Id = largestId + 1;
        fileContext.Forums.Add(newForumItem);
        await fileContext.SaveChangesAsync();
    }
}
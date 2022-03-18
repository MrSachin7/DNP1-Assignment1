using Application;
using Entities.Models;

namespace JsonDataAccess;

public class ForumDAOImpl : IForumDAO {
    private JsonForumContext fileContext;

    public ForumDAOImpl(JsonForumContext FileContext) {
        this.fileContext = FileContext;
    }

    public async Task<List<Forum>> GetAllForumsAsync() {
        return fileContext.Forums.ToList();
    }

    public async Task<SubForum?> GetSubForumAsync(int forumId, int subForumId) {
        Forum forum = await GetForumByIdAsync(forumId);
        return forum.AllSubForums.First(subForum => subForum.Id == subForumId);
    }

    public async Task AddSubForumAsync(SubForum newSubForumItem, int forumId) {
        Forum forumById = await GetForumByIdAsync(forumId);
        if (forumById.AllSubForums.Any()) {
            int largestId = forumById.AllSubForums.Max(subForum => subForum.Id);
            newSubForumItem.Id = largestId + 1;
        }
        else {
            newSubForumItem.Id = 1;
        }

        foreach (Forum forum in fileContext.Forums.Where(forum => forum.Id == forumId)) {
            forum.AllSubForums.Add(newSubForumItem);
        }

        await fileContext.SaveChangesAsync();
    }

    public async Task AddForumAsync(Forum newForumItem) {
        //  Console.WriteLine("ForumDAO");
        if (fileContext.Forums.Any()) {
            int largestId = fileContext.Forums.Max(forum => forum.Id);
            newForumItem.Id = largestId + 1;
        }
        else {
            newForumItem.Id = 1;
        }

        fileContext.Forums.Add(newForumItem);
        await fileContext.SaveChangesAsync();
    }

    public async Task<Forum> GetForumByIdAsync(int id) {
        return fileContext.Forums.First(forum => forum.Id == id);
    }
}
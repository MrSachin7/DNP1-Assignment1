using Application;
using Entities.Models;

namespace JsonDataAccess;

public class ForumDAOImpl : IForumDAO {
    private JsonForumContext fileContext;
    public ForumDAOImpl(JsonForumContext FileContext) {
        this.fileContext = FileContext;
    }

    public async Task<List<Forum>> GetAllForums() {
        return fileContext.Forums.ToList();
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
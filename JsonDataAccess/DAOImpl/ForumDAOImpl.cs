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

        fileContext.Forums.First(forum => forum.Id == forumId).AllSubForums.Add(newSubForumItem);
        await fileContext.SaveChangesAsync();
    }

    public async Task IncrementViewOfForumAsync(int forumId) {
        (await GetForumByIdAsync(forumId)).Views++;
        await fileContext.SaveChangesAsync();
    }

    public async Task AddPostAsync(Post newPostItem, int forumId, int subForumId) {
        SubForum? subForum = (await GetSubForumAsync(forumId, subForumId));
        if (subForum.AllPosts.Any()) {
            int largestId = subForum.AllPosts.Max(post => post.Id);
            newPostItem.Id = largestId + 1;
        }
        else {
            newPostItem.Id = 1;
        }

        newPostItem.CreatedAt = DateTime.Now;
        fileContext.Forums.First(forum => forum.Id == forumId).AllSubForums.First(subForum => subForum.Id == subForumId)
            .AllPosts.Add(newPostItem);
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
using System.Runtime.CompilerServices;
using System.Text.Json;
using Entities.Models;

namespace JsonDataAccess; 

public class JsonForumContext : IDisposable{
    private string forumPath = "forums.json";
    private ICollection<Forum>? forums;

    public ICollection<Forum> Forums {
        get {
            if (forums==null) {
                LoadData();
            }

            return forums;
        }
    }

    public JsonForumContext() {
        if (File.Exists(forumPath)) {
            LoadData();
        }
        else {
            createFile();
        }
    }

    private void createFile() {
        forums = new List<Forum>();
        Task.FromResult(SaveChangesAsync());
    }

    public async Task SaveChangesAsync() {
        string forumAsJson = JsonSerializer.Serialize(forums, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false

        });
        await File.WriteAllTextAsync(forumPath, forumAsJson);
        forums = null;
    }

    private void LoadData() {
        string forumAsJson = File.ReadAllText(forumPath);
        forums = JsonSerializer.Deserialize<List<Forum>>(forumAsJson);
    }

    public void Dispose() {
        forums = null;
    }
}
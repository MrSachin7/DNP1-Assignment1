using System.Text.Json;
using Entities;
using Entities.Models;

namespace JsonDataAccess; 

public class JsonUserContext {
    private string userPath = "users.json";
    private ICollection<User>? users;

    public ICollection<User> Users {
        get {
            if (users==null) {
                LoadData();
            }

            return users!; 
        }
    }


    public JsonUserContext() {
        if (File.Exists(userPath)) {
            LoadData();
        }
        else {
            CreateFile();
        }
    }

    private void LoadData() {
        string usersAsJson = File.ReadAllText(userPath);
        users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
    }

    private void CreateFile() {
        users = new List<User>();
        Task.FromResult(SaveChangesAsync());
    }

    public async Task SaveChangesAsync() {
        string userAsJson = JsonSerializer.Serialize(users, new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNameCaseInsensitive = false
        });
        await File.WriteAllTextAsync(userPath, userAsJson);
        users = null;
    }
}
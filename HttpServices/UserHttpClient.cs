using System.Text;
using System.Text.Json;
using Contracts;
using Entities.Models;

namespace HttpServices;

public class UserHttpClient : IUserService {
    public async Task CreateUserAsync(string username, string password) {
        User user = new User(username, password);
        string userAsJson = JsonSerializer.Serialize(user);
        using HttpClient client = new HttpClient();
        StringContent content = new StringContent(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync("https://localhost:7028/User", content);
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error : {responseMessage.StatusCode} , {responseContent}");
        }
    }

    public async Task<User> GetUserAsync(string username) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7028/User/{username}");
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error : {responseMessage.StatusCode}, {responseContent}");
        }
        User user = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        }) !;
        return user;

    }
}
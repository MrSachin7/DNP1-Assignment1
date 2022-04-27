﻿using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using Contracts;
using Entities.Models;

namespace HttpServices;

public class ForumHttpClient : IForumService {
    public async Task<Forum> AddForumAsync(Forum newForumItem) {
        using HttpClient client = new HttpClient();
        string forumAsJson = JsonSerializer.Serialize(newForumItem);
        StringContent content = new StringContent(forumAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage = await client.PostAsync($"https://localhost:7028/Forum", content);
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        Forum forumFromServer = GetDeserialized<Forum>(responseContent);
        return forumFromServer;
    }

    public async Task<Forum> GetForumByIdAsync(int id) {
        using HttpClient client = new();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7028/Forum/{id}");
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        Forum forumFromServer = GetDeserialized<Forum>(responseContent);
        return forumFromServer;
    }


    public async Task<List<Forum>> GetAllForumsAsync() {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:7028/Forum");
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        List<Forum> allForumsFromServer = GetDeserialized<List<Forum>>(responseContent);
        return allForumsFromServer;
    }

    public async Task<SubForum?> GetSubForumAsync(int forumId, int subForumId) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage =
            await client.GetAsync($"https://localhost:7028/Forum/{forumId}/{subForumId}");
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        SubForum subForumFromServer = GetDeserialized<SubForum>(responseContent);
        return subForumFromServer;
    }

    public async Task<SubForum> AddSubForumAsync(SubForum newSubForumItem, int forumId) {
        using HttpClient client = new HttpClient();
        string subForumAsJson = JsonSerializer.Serialize(newSubForumItem);
        StringContent content = new StringContent(subForumAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage =
            await client.PostAsync($"https://localhost:7028/Forum/{forumId}", content);
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        SubForum subForumFromServer = GetDeserialized<SubForum>(responseContent);
        return subForumFromServer;
    }

    public async Task IncrementViewOfForumAsync(int forumId) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage =
            await client.PatchAsync($"https://localhost:7028/Forum/IncrementView/{forumId}",
                null); //TODO ask troels about the null here...
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
    }

    public async Task<Post> AddPostAsync(Post newPostItem, int forumId, int subForumId) {
        using HttpClient client = new HttpClient();
        string postAsJson = JsonSerializer.Serialize(newPostItem);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage =
            await client.PostAsync($"https://localhost:7028/Forum/{forumId}/{subForumId}", content);
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        Post postFromServer = GetDeserialized<Post>(responseContent);
        return postFromServer;
    }

    public async Task IncrementViewOfSubForumAsync(int forumId, int subForumId) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage =
            await client.PatchAsync($"https://localhost:7028/Forum/IncrementView/{forumId}/{subForumId}", null);
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
    }

    public async Task<Post?> GetPostAsync(int forumId, int subForumId, int postId) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage =
            await client.GetAsync($"https://localhost:7028/Forum/{forumId}/{subForumId}/{postId}");
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        Post postFromServer = GetDeserialized<Post>(responseContent);
        return postFromServer;
    }

    public async Task<Comment> AddCommentToPost(int forumId, int subForumId, int postId, Comment commentToPost) {
        using HttpClient client = new HttpClient();
        string commentAsJson = JsonSerializer.Serialize(commentToPost);
        StringContent content = new StringContent(commentAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage =
            await client.PostAsync($"https://localhost:7028/Forum/{forumId}/{subForumId}/{postId}", content);
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        Comment commentFromServer = GetDeserialized<Comment>(responseContent);
        return commentFromServer;
    }

    public async Task<Comment> EditComment(int forumId, int subForumId, int postId, Comment editedComment) {
        using HttpClient client = new HttpClient();
        string commentAsJson = JsonSerializer.Serialize(editedComment);
        StringContent content = new StringContent(commentAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage responseMessage =
            await client.PutAsync($"https://localhost:7028/Forum/{forumId}/{subForumId}/{postId}", content);
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        Comment commentFromServer = GetDeserialized<Comment>(responseContent);
        return commentFromServer;
    }

    public async Task<Comment> DeleteComment(int forumId, int subForumId, int postId, int commentId) {
        using HttpClient client = new HttpClient();
        HttpResponseMessage responseMessage =
            await client.DeleteAsync($"https://localhost:7028/Forum/{forumId}/{subForumId}/{postId}/{commentId}");
        string responseContent =
            await GetResponseContentFromResponseMessageAndThrowAppropriateException(responseMessage);
        Comment comment = GetDeserialized<Comment>(responseContent);
        return comment;
    }

    private T GetDeserialized<T>(string jsonFormat) {
        T obj = JsonSerializer.Deserialize<T>(jsonFormat, new JsonSerializerOptions() {
            PropertyNameCaseInsensitive = true
        }) !;
        return obj;
    }

    private async Task<string> GetResponseContentFromResponseMessageAndThrowAppropriateException(
        HttpResponseMessage responseMessage) {
        string responseContent = await responseMessage.Content.ReadAsStringAsync();
        if (!responseMessage.IsSuccessStatusCode) {
            throw new Exception($"Error :{responseMessage.StatusCode}, {responseContent}");
        }

        return responseContent;
    }
}
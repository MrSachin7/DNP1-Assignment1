﻿using Entities.Models;

namespace Contracts; 

public interface IForumService {
    // Task<List<Post>> GetAllPostsAsync();
    Task AddForumAsync(Forum newForumItem);
    Task<Forum> GetForumByIdAsync(int id);
}
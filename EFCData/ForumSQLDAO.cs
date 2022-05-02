using System.Net.Sockets;
using Application;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace EFCData;

public class ForumSQLDAO : IForumDAO {
    private readonly ForumContext context;

    public ForumSQLDAO(ForumContext context) {
        this.context = context;
    }


    public async Task<Forum> AddForumAsync(Forum newForumItem) {
        EntityEntry<Forum> added = await context.Forums.AddAsync(newForumItem);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public async Task<Forum> GetForumByIdAsync(int id) {
        Forum forum = context.Forums.Include(forum => forum.AllSubForums)
            .ThenInclude(subForum => subForum.AllPosts) // todo ask troels how to also include OwnedBy here....
            .ThenInclude(post => post.Comments)
            .Include(forum1 => forum1.AllSubForums)
            .ThenInclude(subForum => subForum.OwnedBy)
            .First(forum => forum.Id == id);
        return forum;
    }

    public async Task<List<Forum>> GetAllForumsAsync() {
        return await context.Forums.ToListAsync();
    }

    public async Task<SubForum?> GetSubForumAsync(int subForumId) {
        SubForum subForum = await context.SubForums.Include(forum => forum.AllPosts).Include(forum => forum.OwnedBy)
            .FirstAsync(forum => forum.Id == subForumId);
        return subForum;
    }

    public async Task<SubForum> AddSubForumAsync(SubForum newSubForumItem, int forumId) {
        Forum? forum = await context.Forums.FindAsync(forumId);
        User? user = await context.Users.FindAsync(newSubForumItem.OwnedBy.Username);
        if (forum is null) {
            throw new Exception($"Cannot find the forum with the id : {forumId}");
        }

        if (user is null) {
            throw new Exception("Invalid username for the user creator");
        }

        newSubForumItem.OwnedBy = user;

        forum.AllSubForums.Add(newSubForumItem); // todo ask troels are these same things ??
        //  newSubForumItem.BelongsTo = forum; // todo ask troels regarding these entry points...
        // EntityEntry<SubForum> entityEntry = await context.SubForums.AddAsync(newSubForumItem);
        await context.SaveChangesAsync();
        return newSubForumItem;
    }

    public async Task IncrementViewOfForumAsync(int forumId) {
        Forum forum = await GetForumByIdAsync(forumId);
        forum.Views++;
        await context.SaveChangesAsync();
    }

    public async Task<Post> AddPostAsync(Post newPostItem, int subForumId) {
        SubForum? subForum = await context.SubForums.FindAsync(subForumId);
        User? user = await context.Users.FindAsync(newPostItem.WrittenBy.Username);


        if (subForum is null) {
            throw new Exception($"Cannot find the subforum with id : {subForumId}");
        }

        newPostItem.WrittenBy = user;
        subForum.AllPosts.Add(newPostItem);

        await context.SaveChangesAsync();
        return newPostItem;
    }

    public async Task IncrementViewOfSubForumAsync(int subForumId) {
        var first = await context.SubForums.FindAsync(subForumId);
        if (first is null) {
            throw new Exception($"Cannot find the subForum with the id ; {subForumId}");
        }

        first.Views++;
        await context.SaveChangesAsync();
    }

    public async Task<Post?> GetPostAsync(int postId) {
        Post? post = context.Posts.Include(post1 => post1.Comments)
            .Include(post1 => post1.WrittenBy)
            .First(post => post.Id == postId);
        return post;
    }

    public async Task<Comment> AddCommentToPost(int postId, Comment commentToPost) {
        Post post = context.Posts.First(post1 => post1.Id == postId);
        User? findAsync = await context.Users.FindAsync(commentToPost.Writer.Username);
        commentToPost.Writer = findAsync!;
        post.Comments.Add(commentToPost);
        await context.SaveChangesAsync();
        return commentToPost;
    }

    public async Task<Comment> EditComment(Comment editedComment) {
        EntityEntry<Comment> update = context.Comments.Update(editedComment);
        await context.SaveChangesAsync();
        return update.Entity;
    }

    public async Task<Comment> DeleteComment(int commentId) {
        Comment? async = await context.Comments.FindAsync(commentId);
        EntityEntry<Comment> entityEntry = context.Comments.Remove(async);
        await context.SaveChangesAsync();
        return entityEntry.Entity;
    }
}
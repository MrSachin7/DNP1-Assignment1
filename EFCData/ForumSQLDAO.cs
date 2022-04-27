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
        Forum forum = context.Forums.Include(forum => forum.AllSubForums).ThenInclude(subForum => subForum.AllPosts)
            .ThenInclude(post => post.Comments).First(forum => forum.Id == id);
        return forum;
    }

    public async Task<List<Forum>> GetAllForumsAsync() {
        return await context.Forums.ToListAsync();
    }

    public async Task<SubForum?> GetSubForumAsync(int subForumId) {
        IIncludableQueryable<SubForum,ICollection<Post>> queryable = context.SubForums.Include(subForum => subForum.AllPosts);
        queryable.Include(forum => forum.OwnedBy);
        queryable.ThenInclude(post => post.Comments);
       return queryable.First(subForum => subForum.Id == subForumId);
        
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
        SubForum? subForum =await context.SubForums.FindAsync(subForumId);
        if (subForum is null) {
            throw new Exception($"Cannot find the subforum with id : {subForumId}");
        }
        subForum.AllPosts.Add(newPostItem);
        await context.SaveChangesAsync();
        return newPostItem;
    }

    public async Task IncrementViewOfSubForumAsync(int subForumId) {
        var first =await context.SubForums.FindAsync(subForumId);
        if (first is null) {
            throw new Exception($"Cannot find the subForum with the id ; {subForumId}");
        }
        first.Views++;
        await context.SaveChangesAsync();
    }

    public async Task<Post?> GetPostAsync(int postId) {
        Post? post = context.Posts.Include(post1 => post1.Comments).First(post => post.Id == postId);
        return post;
    }

    public async Task<Comment> AddCommentToPost(int postId, Comment commentToPost) {
        Post post = context.Posts.First(post1 => post1.Id == postId);
        post.Comments.Add(commentToPost);
        await context.SaveChangesAsync();
        return commentToPost;
    }

    public async Task<Comment> EditComment(Comment editedComment) {
        Comment comment = context.Comments.First(comment1 => comment1.Id == editedComment.Id);
        comment.Body = editedComment.Body;
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment> DeleteComment(int commentId) {
        Comment comment = await context.Comments.FirstAsync(comment1 => comment1.Id == commentId);
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task<Post> AddPostAsync(Post newPostItem, int forumId, int subForumId) {
        EntityEntry<Post> entityEntry = await context.Posts.AddAsync(newPostItem);
        await context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task IncrementViewOfSubForumAsync(int forumId, int subForumId) {
        SubForum subForum = context.SubForums.First(subForum => subForum.Id == subForumId);
        subForum.Views++;
        await context.SaveChangesAsync();
    }
}
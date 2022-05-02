using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCData; 

public class ForumContext :DbContext
{
    public DbSet<Forum> Forums { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<SubForum> SubForums { get; set; }
    public DbSet<Post> Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite(
            @"Data Source =D:\SEM 3\SEM3\DNP1\DNP1-Assignment-1\DNPFirstAssignment\WebApi\Forums.db");
    }
}
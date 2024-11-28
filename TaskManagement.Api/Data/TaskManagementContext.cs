using Microsoft.EntityFrameworkCore;
using System;
using TaskManagement.Api.Entities;
public class TaskManagementContext(DbContextOptions<TaskManagementContext> options) : DbContext(options)
{

    public DbSet<User> Users => Set<User>();
    public DbSet<TaskManagement.Api.Entities.Task> Tasks => Set<TaskManagement.Api.Entities.Task>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>().HasData(
             new { Id = 1, UserName = "admin", Password = "hashedpassword" }
         );

        // defult data
        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Work", Description = "Work related tasks", UserId = 1 },
            new { Id = 2, Name = "Personal", Description = "Personal tasks", UserId = 1 }
        );



    }
}

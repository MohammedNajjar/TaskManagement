using System;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Entities;

namespace TaskManagement.Api.Data;

public class TaskManagementContext(DbContextOptions<TaskManagementContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Entities.Task> Tasks => Set<Entities.Task>();
    public DbSet<Category> Categories => Set<Category>();
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(


            new { Id = 1, Name = "Work", Description = "Work related tasks" },
            new { Id = 2, Name = "Personal", Description = "Personal tasks" },
            new { Id = 3, Name = "Shopping", Description = "Shopping list" },
            new { Id = 4, Name = "Health", Description = "Health and fitness" },
            new { Id = 5, Name = "Study", Description = "Educational tasks" }
        );


    }

}

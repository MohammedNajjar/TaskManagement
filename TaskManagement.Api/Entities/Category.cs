using System;

namespace TaskManagement.Api.Entities;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; } 
    public ICollection<Task>? Tasks { get; set; }
}

using System;
using Mapster;

namespace TaskManagement.Api.Entities;
[AdaptTo("[name]Dto"), GenerateMapper]
public class Category

{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; } 
    public ICollection<Task>? Tasks { get; set; }
}

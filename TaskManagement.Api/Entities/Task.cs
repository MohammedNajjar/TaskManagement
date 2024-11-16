using System;

namespace TaskManagement.Api.Entities;

public class Task
{

    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateOnly DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public int Priority { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

}

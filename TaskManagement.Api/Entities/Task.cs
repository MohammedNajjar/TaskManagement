using System;
using Mapster;
using Microsoft.VisualBasic;

namespace TaskManagement.Api.Entities;
[AdaptTo("[name]Dto"), GenerateMapper]
public class Task
{

    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }=false;
    public int Priority { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public string? Status { get;  set; }
}

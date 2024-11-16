namespace TaskManagement.Api.Dtos.TaskDtos;

public record class TaskDetailsDto
(
    int Id,
    string Title,
    string? Description,
    string Status, // Pending, In Progress, Completed
    string Priority, // Low, Medium, High
    DateOnly DueDate,
    string CategoryName
);

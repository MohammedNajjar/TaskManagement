namespace TaskManagement.Api.Dtos.TaskDtos;

public record class TaskSummaryDto
(
    int Id,
    string Title,
    string Description,
    bool IsCompleted,
    int Priority,
    DateOnly DueDate,
    string User,
    string Category,
    string Status

);

namespace TaskManagement.Api.Dtos.TaskDtos;

public record class TaskDetailsDto
(
 int Id,
    string Title,
    string Description,
    bool IsCompleted,
    int Priority,
    DateOnly DueDate,
    int UserId,
    int CategoryId,
    string Status
);

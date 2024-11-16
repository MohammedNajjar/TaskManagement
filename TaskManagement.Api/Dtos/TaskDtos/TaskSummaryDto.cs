namespace TaskManagement.Api.Dtos.TaskDtos;

public record class TaskSummaryDto
(
    int Id,
    string Title,
    string Priority,
    DateTime DueDate
);

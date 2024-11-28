using System;
using TaskManagement.Api.Data;
using TaskManagement.Api.Dtos.TaskDtos;
using TaskManagement.Api.Entities;

namespace TaskManagement.Api.Mapping.Task;

public static class TaskMapping
{
    public static Entities.Task ToEntity(this CreateTaskDto createTask, TaskManagementContext dbContext)
    {
        if (createTask.UserId <= 0 || createTask.CategoryId <= 0)
            throw new ArgumentException("Invalid UserId or CategoryId");
        var userExists = dbContext.Users.Any(u => u.Id == createTask.UserId);
        var categoryExists = dbContext.Categories.Any(c => c.Id == createTask.CategoryId);

        if (!userExists)
            throw new ArgumentException($"User with ID {createTask.UserId} does not exist.");
        if (!categoryExists)
            throw new ArgumentException($"Category with ID {createTask.CategoryId} does not exist.");

        return new Entities.Task
        {
            Title = createTask.Title,
            Description = createTask.Description,
            DueDate = createTask.DueDate,
            IsCompleted = createTask.IsCompleted,
            Priority = createTask.Priority,
            UserId = createTask.UserId,
            CategoryId = createTask.CategoryId,
            Status = createTask.Status ?? "Pending"
        };
    }

    public static Entities.Task ToTaskEntity(this UpdateTaskDto updateTask, int id, TaskManagementContext dbContext)
    {
        var userExists = dbContext.Users.Any(u => u.Id == updateTask.UserId);
        var categoryExists = dbContext.Categories.Any(c => c.Id == updateTask.CategoryId);

        if (!userExists)
            throw new ArgumentException($"User with ID {updateTask.UserId} does not exist.");
        if (!categoryExists)
            throw new ArgumentException($"Category with ID {updateTask.CategoryId} does not exist.");


        return new Entities.Task()
        {
            Id = id,
            Title = updateTask.Title,
            Description = updateTask.Description,
            DueDate = updateTask.DueDate,
            IsCompleted = updateTask.IsCompleted,
            Priority = updateTask.Priority,
            UserId = updateTask.UserId,
            CategoryId = updateTask.CategoryId,
            Status = updateTask.Status

        };
    }


    public static TaskSummaryDto ToSummaryDto(this Entities.Task task)
    {
        return new TaskSummaryDto(
     task.Id,
     task.Title,
     task.Description,
     task.IsCompleted,
     task.Priority,
     task.DueDate,
     task.User?.UserName ?? "Unknown",
     task.Category?.Name ?? "Uncategorized",
     task.Status ?? "Pending"
        );
    }

    public static TaskDetailsDto ToDetailsDto(this Entities.Task task)
    {
        return new(
            task.Id,
            task.Title,
            task.Description,
            task.IsCompleted,
            task.Priority,
            task.DueDate,
            task.UserId,
            task.CategoryId,
            task.Status ?? "Pending"
        );
    }





}




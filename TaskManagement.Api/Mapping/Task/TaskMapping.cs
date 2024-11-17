using System;
using TaskManagement.Api.Dtos.TaskDtos;
using TaskManagement.Api.Entities;

namespace TaskManagement.Api.Mapping.Task;

public static class TaskMapping
{
    public static Entities.Task ToEntity(this CreateTaskDto createTask)
    {
        return new Entities.Task()
        {
            Title = createTask.Title,
            Description = createTask.Description,
            DueDate = createTask.DueDate,
            IsCompleted = createTask.IsCompleted,
            Priority = createTask.Priority,
            UserId = createTask.UserId,
            CategoryId = createTask.CategoryId,
            Status = createTask.Status

        };
    }

    public static Entities.Task ToEntity(this UpdateTaskDto updateTask, int id)
    {

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
            task.Priority ?? "Low",   
            task.DueDate,             
            task.User!.UserName,              
            task.Category!.Name,          
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
            task.Priority ?? "Low",   
            task.DueDate,             
            task.UserId,            
            task.CategoryId,          
            task.Status ?? "Pending"  
        );
    }





}




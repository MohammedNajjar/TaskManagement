using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Data;
using TaskManagement.Api.Dtos.TaskDtos;
using TaskManagement.Api.Entities;
using TaskManagement.Api.Mapping.Task;




namespace TaskManagement.Api.Endpoints;

public static class TaskEndpoints
{

    public static RouteGroupBuilder MapTaskEndpoint(this WebApplication app)
    {
        const string getTaskEndpointName = "GetTask";
        var group = app.MapGroup("tasks").WithParameterValidation().RequireAuthorization();
        // post /tasks

        group.MapPost("", async (CreateTaskDto creatTask, TaskManagementContext dbcontext) =>
        {
            Entities.Task task = creatTask.ToEntity(dbcontext);
            dbcontext.Add(task);
            await dbcontext.SaveChangesAsync();
            return Results.CreatedAtRoute(getTaskEndpointName, new { id = task.Id }, task.ToDetailsDto());
        });

        // GET / tasks
        group.MapGet("/", async (TaskManagementContext dbContext, string? status, string? category, int? priority) =>
   {
       IQueryable<Entities.Task> query = dbContext.Tasks;

       //  خيارات التصفية
       if (!string.IsNullOrEmpty(status))
           query = query.Where(task => task.Status != null && task.Status.ToLower() == status.ToLower());

       if (!string.IsNullOrEmpty(category))
           query = query.Where(task => task.Category != null && task.Category.Name.ToLower() == category.ToLower());

       if (priority.HasValue)
           query = query.Where(task => task.Priority == priority);

       var tasks = await query.Include(task => task.Category).Include(task => task.User)
                               .OrderBy(task => task.DueDate)
                               .Select(task => task.ToSummaryDto())
                               .AsNoTracking()
                               .ToListAsync();

       if (!tasks.Any()) // إذا كانت القائمة فارغة
       {
           return Results.Ok(new { message = "No tasks found. Create a task to get started." });
       }

       return Results.Ok(tasks);
   });

        // GET / tasks / id

        group.MapGet("/{id}", async (int id, TaskManagementContext dbcontext) =>
        {
            Entities.Task? task = await dbcontext.Tasks
            .Include(t => t.Category)
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);

            return task is null ? Results.NotFound(new { message = "Task not found" }) : Results.Ok(task.ToDetailsDto());
        }

        ).WithName(getTaskEndpointName);

        //PUT /tasks/{id}

        group.MapPut("/{id}", async (int id, UpdateTaskDto updateTask, TaskManagementContext dpContext) =>
        {
            var existingTask = await dpContext.Tasks.FindAsync(id);
            if (existingTask is null)
            {
                return Results.NotFound(new { message = "Task not found" });
            }
            dpContext.Entry(existingTask).CurrentValues.
            SetValues(updateTask.ToTaskEntity(id, dpContext));
            await dpContext.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        // DELETE /api/tasks/{id}
        group.MapDelete("/{id:int}", async (int id, TaskManagementContext dbContext) =>
       {
           var taskExists = await dbContext.Tasks.AnyAsync(t => t.Id == id);
           if (!taskExists)
           {
               return Results.NotFound(new
               {
                   message = $"Task with ID {id} was not found in the system."
               });
           }

           await dbContext.Tasks.Where(tasks => tasks.Id == id).ExecuteDeleteAsync();
           return Results.NoContent();
       });
        return group;
    }
}

using System;
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
        var group = app.MapGroup("tasks").WithParameterValidation();
        // post /tasks
        group.MapPost("", async (CreateTaskDto creatTask, TaskManagementContext dbcontext) =>
        {
            Entities.Task task = creatTask.ToEntity();
            dbcontext.Add(task);
            await dbcontext.SaveChangesAsync();
            return Results.CreatedAtRoute("GetUserById", new { id = task.Id }, task.ToDetailsDto());
        });
        // GET / tasks
        group.MapGet("/", async (TaskManagementContext dbcontext) =>
        await dbcontext.Tasks
        .Include(task => task.Category).
        Select(task => task.ToSummaryDto()).
        AsNoTracking().
        ToListAsync());

        // GET / tasks / id

        group.MapGet("/{id}", async (int id, TaskManagementContext dbcontext) =>
        {
            Entities.Task? task = await dbcontext.Tasks.FindAsync(id);
            return task is null ? Results.NoContent() : Results.Ok(task.ToDetailsDto());
        }

        ).WithName(getTaskEndpointName);

        //PUT /tasks/{id}

        group.MapPut("/{id}", async (int id, UpdateTaskDto updateTask, TaskManagementContext dpContext) =>
        {
            var existingTask = await dpContext.Tasks.FindAsync(id);
            if (existingTask is null)
            {
                return Results.NotFound();
            }
            dpContext.Entry(existingTask).CurrentValues
                    .SetValues(updateTask.ToTaskEntity(id));
            await dpContext.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        // DELETE /api/tasks/{id}
        group.MapDelete("/{id}", async (int id, TaskManagementContext dbContext) =>
       {
           await dbContext.
           Tasks.Where(tasks => tasks.Id == id).
           ExecuteDeleteAsync();
           return Results.NoContent();
       }
       );




        return group;
    }
}

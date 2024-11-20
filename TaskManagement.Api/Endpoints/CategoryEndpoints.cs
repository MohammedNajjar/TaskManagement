using System;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Data;
using TaskManagement.Api.Dtos.CategoryDtos;
using TaskManagement.Api.Entities;
using TaskManagement.Api.Mapping.Category;


namespace TaskManagement.Api.Endpoints;
//POST /api/categories:
//GET /api/categories:
//PUT /api/categories/{id}:
//DELETE /api/categories/{id}:
public static class CategoryEndpoints
{
    const string getCategoryEndpointName = "GetCategory";

    public static RouteGroupBuilder MapCategoryEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("categorys").WithParameterValidation();

        //POST /api/categories:
        group.MapPost("", async (CreateCategoryDto creatCategory, TaskManagementContext dbcontext) =>
        {
            Category category = creatCategory.ToEntitiy();
            dbcontext.Add(category);
            await dbcontext.SaveChangesAsync();
            return Results.CreatedAtRoute(null, new { id = category.Id }, category.ToCategoryDetails());

        }
        );

        //GET /categories

        group.MapGet("", async (TaskManagementContext dbcontext) =>
        await dbcontext.Categories.
        //Include(category => category.Tasks).
        Select(category => category.ToCategorySummary()).
        AsNoTracking().
        ToListAsync());

        //Get /categories/{id}

        group.MapGet("/{id}", async (int id, TaskManagementContext dbcontext) =>
           { var category = await dbcontext.Categories.FindAsync(id);
        return category is null ? Results.NotFound() :
          Results.Ok(category.ToCategoryDetails());


    }).WithName(getCategoryEndpointName);


        //PUT /categories/{id}

        group.MapPut("/{id}", async (int id, UpdateCategoryDto updateCategory, TaskManagementContext dpContext) =>
        {
            var existingCategory = await dpContext.Categories.FindAsync(id);
            if (existingCategory is null)
            {
                return Results.NotFound();
            }
            dpContext.Entry(existingCategory).CurrentValues
                    .SetValues(updateCategory.ToEntity(id));
            await dpContext.SaveChangesAsync();

            return Results.NoContent();
        }
        );

        //DELETE /api/categories/{id}

        group.MapDelete("/{id}", async (int id, TaskManagementContext dbContext) =>
        {
            await dbContext.Tasks.Where(category => category.Id == id).
        ExecuteDeleteAsync();
            return Results.NoContent();
        }
        );








        return group;
    }

}

/*
using TaskManagement.Data;
using TaskManagement.Dtos;
using TaskManagement.Entities;
using TaskManagement.Mapping;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Data;
using TaskManagement.Api.Mapping.Task;
namespace TaskManagement.Endpoints;
public static class TaskEndpoints

{
    const string getTaskEndpointName = "GetTask";
    public static RouteGroupBuilder MapTaskEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("tasks").WithParameterValidation();
        // Get /tasks
        group.MapGet("/", async (TaskManagementContext dbContext)
        => await dbContext.Tasks.Include(task => task.Category).
        Select(task => task.ToSummaryDto()).AsNoTracking().ToListAsync());
        // Get /tasks/1
        group.MapGet("/{id}", async (int id, TaskManagementContext dbContext) =>
        {
            var task = await dbContext.Tasks.FindAsync(id);
            return task is null ? Results.NotFound() :
              Results.Ok(task.ToDetailsDto());
        }).
          WithName(getTaskEndpointName);
        // Post /tasks
        group.MapPost("", async (Api.Dtos.TaskDtos.CreateTaskDto newTask, TaskManagementContext dbContext) =>
        {
            var task = newTask.ToEntity();
            dbContext.Tasks.Add(task);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(getTaskEndpointName,
                 new { id = task.Id }, task.ToDetailsDto());
        }); // Put /tasks/1
        group.MapPut("/{id}", async (int id, Api.Dtos.TaskDtos.UpdateTaskDto updateTask,
        TaskManagementContext dbContext) =>
         {
             var existingTask = await dbContext.Tasks.FindAsync(id);
             if (existingTask is null) { return Results.NotFound(); }
             dbContext.Entry(existingTask).CurrentValues.SetValues(updateTask.ToEntity(id));
             await dbContext.SaveChangesAsync(); return Results.NoContent();
         }); // Delete /tasks/1 
        group.MapDelete("/{id}", async (int id, TaskManagementContext dbContext) =>
        {
            await dbContext.Tasks.Where(task => task.Id == id).
        ExecuteDeleteAsync();
            return Results.NoContent();
        }
        );
        return group;
*/
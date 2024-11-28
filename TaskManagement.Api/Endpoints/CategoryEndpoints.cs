using System;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Data;
using TaskManagement.Api.Dtos.CategoryDtos;
using TaskManagement.Api.Entities;
using TaskManagement.Api.Mapping.Category;


namespace TaskManagement.Api.Endpoints;
public static class CategoryEndpoints
{
    const string getCategoryEndpointName = "GetCategory";

    public static RouteGroupBuilder MapCategoryEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("categories").WithParameterValidation();

        //POST /api/categories:
        group.MapPost("", async (CreateCategoryDto creatCategory, TaskManagementContext dbcontext) =>
        {
            var user = await dbcontext.Users.FindAsync(creatCategory.UserId);
            if (user == null)
            {
                return Results.NotFound(new { message = "User not found" });
            }
            Category category = creatCategory.ToEntitiy();
            dbcontext.Add(category);
            await dbcontext.SaveChangesAsync();
            return Results.CreatedAtRoute(getCategoryEndpointName, new { id = category.Id }, category.ToCategoryDetails());

        }
        );

        //GET /categories

        group.MapGet("/", async (TaskManagementContext dbcontext) =>
        await dbcontext.Categories.
        Include(category => category.Tasks).
        Select(category => category.ToCategorySummary()).
        AsNoTracking().
        ToListAsync());

        //Get /categories/{id}

        group.MapGet("/{id}", async (int id, TaskManagementContext dbcontext) =>
           {
               var category = await dbcontext.Categories
                .Include(category => category.Tasks)
                .FirstOrDefaultAsync(c => c.Id == id);
               return category is null ? Results.NotFound(new { message = "Category not found" }) :
                 Results.Ok(category.ToCategoryDetails());


           }).WithName(getCategoryEndpointName);


        //PUT /categories/{id}
        group.MapPut("/{id}", async (int id, UpdateCategoryDto updateCategory, TaskManagementContext dpContext) =>
   {
       if (updateCategory.UserId is null)
       {
           return Results.BadRequest(new { message = "UserId is required" });
       }

       var existingCategory = await dpContext.Categories.FindAsync(id);
       if (existingCategory is null)
       {
           return Results.NotFound(new { message = "Category not found" });
       }

       var user = await dpContext.Users.FindAsync(updateCategory.UserId.Value);
       if (user is null)
       {
           return Results.BadRequest(new { message = "User not found" });
       }

       dpContext.Entry(existingCategory).CurrentValues.SetValues(updateCategory.ToEntity(id));
       await dpContext.SaveChangesAsync();

       return Results.NoContent();
   });



        //DELETE /api/categories/{id}

        group.MapDelete("/{id}", async (int id, TaskManagementContext dbContext) =>
 {
     var category = await dbContext.Categories.FindAsync(id);
     if (category is null)
     {
         return Results.NotFound(new { message = "Category not found" });
     }
     await dbContext.Categories
                   .Where(category => category.Id == id)
                   .ExecuteDeleteAsync();

     return Results.NoContent();

 });
        return group;
    }

}


using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Data;
using TaskManagement.Api.Dtos.UserDtos;
using TaskManagement.Api.Entities;
using TaskManagement.Api.Mapping.User;

namespace TaskManagement.Api.Endpoints;

public static class UserEndpoints
//POST /api/auth/register
//POST /api/auth/login
{
    // const string getTaskEndpointName = "GetTask";
// تعمل  methode RouteGroubBuilder للكل اويب 
//
    public static RouteGroupBuilder MapUserEndpoints(this WebApplication app){
        var group = app.MapGroup("users").WithParameterValidation();


        group.MapPost("/register", async (UserRegisterDto userRegister, TaskManagementContext dbContext) =>
    {
        User user = userRegister.ToEntity();
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

    return Results.CreatedAtRoute("GetUserById", new { id = user.Id }, user.ToUserProfileDto());
    });
        // group.MapPost("/login", async (UserLoginDto userLogin, TaskManagementContext dbContext) =>{
        //     var user = await dbContext.Users.SingleOrDefaultAsync(u => u.UserName == userLogin.Username);
        //     dbContext.Users.Add(user);
        //     await dbContext.SaveChangesAsync();

        //     // if (user is null || !VerifyPassword(userLogin.Password, user.Password))
        //     // {
        //     //     return Results.Unauthorized();


        //     // }
        // }
    
        // );
     
    
     


        return group;

    }
  


}

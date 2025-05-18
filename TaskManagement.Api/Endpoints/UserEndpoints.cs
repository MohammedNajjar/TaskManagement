using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Data;
using TaskManagement.Api.Dtos.UserDtos;
using TaskManagement.Api.Entities;


namespace TaskManagement.Api.Endpoints
{
    public static class UserEndpoints
    {
        const string getUserEndpointName = "GetUser";

        public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("users").WithParameterValidation();

            // creat post register
            group.MapPost("/register", async (UserRegisterDto userRegister, TaskManagementContext dbContext) =>
            {
                // تحويل DTO إلى كائن مستخدم
                User user = userRegister.Adapt<User>();
                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, userRegister.Password);

                if (await dbContext.Users.AnyAsync(u => u.UserName == userRegister.UserName))
                {
                    return Results.BadRequest(new { message = "Username is already taken." });
                }

                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
                var userProfile = user.Adapt<UserProfileDto>();
                return Results.CreatedAtRoute(getUserEndpointName, new { id = user.Id }, userProfile);
            });

            // Get User/{id}
            group.MapGet("/{id}", async (int id, TaskManagementContext dbContext) =>
            {
                var user = await dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return Results.NotFound();
                }
                var userProfile = user.Adapt<UserProfileDto>();
                return Results.Ok(userProfile);
            }).WithName(getUserEndpointName);

            // creat post login
            group.MapPost("/login", async (UserLoginDto userLogin, TaskManagementContext dbContext, IConfiguration configuration) =>
            {

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userLogin.Username);

                if (user == null || string.IsNullOrEmpty(user.Password))
                {
                    return Results.Json(new { message = "Invalid username or password" }, statusCode: 401);
                }

                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, userLogin.Password);

                if (result != PasswordVerificationResult.Success)
                {
                    return Results.Json(new { message = "Invalid username or password" }, statusCode: 401);
                }

                // توليد JWT Token 
                var token = JwtHelper.GenerateToken(user, configuration);
                return Results.Ok(new { Token = token });
            });

            return group;
        }
    }
}

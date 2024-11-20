using TaskManagement.Api.Data;
using TaskManagement.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("TaskManagement");
builder.Services.AddSqlite<TaskManagementContext>(connString);

// Add other necessary services here, like controllers, authentication, etc.
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoints();
app.MapControllers();
await app.MigrateDbAsync();
app.Run();

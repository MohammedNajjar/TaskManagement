using TaskManagement.Api.Data;
using TaskManagement.Api.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Mapster;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
var connString = builder.Configuration.GetConnectionString("TaskManagement");
builder.Services.AddSqlite<TaskManagementContext>(connString);

// JWT Configuration
var secretKey = builder.Configuration["Jwt:Secret"];
if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("JWT SecretKey is not configured in appsettings.json");
}

var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];
if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
{
    throw new InvalidOperationException("JWT Issuer or Audience is not configured in appsettings.json");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Mapster Configuration
var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(Program).Assembly);
builder.Services.AddSingleton(config); // Register TypeAdapterConfig
builder.Services.AddScoped<IMapper, ServiceMapper>(); // Register ServiceMapper


// Additional Services
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Map Endpoints
app.MapCategoryEndpoint();
app.MapUserEndpoints();
app.MapTaskEndpoint();
app.MapControllers();

// Apply Database Migrations
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagementContext>();
await dbContext.Database.MigrateAsync();

app.Run();

using System;
using Microsoft.EntityFrameworkCore;


namespace TaskManagement.Api.Data;

public static class DataExtention
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagementContext>();
        await dbContext.Database.MigrateAsync();
    }

}

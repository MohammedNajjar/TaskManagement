using Mapster;
using TaskManagement.Api.Dtos.TaskDtos;
using TaskManagement.Api.Entities;

public class TaskMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TaskManagement.Api.Entities.Task, TaskSummaryDto>()
            .Map(dest => dest.User, src => src.User != null ? src.User.UserName : "Unknown")
            .Map(dest => dest.Category, src => src.Category != null ? src.Category.Name : "Uncategorized");
    }
}
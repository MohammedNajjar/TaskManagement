using TaskManagement.Api.Dtos.TaskDtos;

namespace TaskManagement.Api.Dtos.CategoryDtos;

public record class CategoryDetailsDto
(
     int Id,
    string Name,
    string? Description,
    int TaskCount
    
);
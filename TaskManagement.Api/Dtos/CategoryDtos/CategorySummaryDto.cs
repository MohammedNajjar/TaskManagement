namespace TaskManagement.Api.Dtos.CategoryDtos;

public record class CategorySummaryDto
(
     int Id,
    string Name,
    string? Description,
    int TaskCount
);

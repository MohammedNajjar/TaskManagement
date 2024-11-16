namespace TaskManagement.Api.Dtos.CategoryDtos;

public record class CategoryDetailsDto
(
    int Id,
    string Name,
    string? Description,
    int TaskCount // عدد المهام المرتبطة بهذه الفئة
);
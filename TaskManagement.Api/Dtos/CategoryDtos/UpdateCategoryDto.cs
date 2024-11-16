using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Dtos.CategoryDtos;

public record class UpdateCategoryDto
(
    [Required][StringLength(50)] string Name,
    [StringLength(200)] string? Description

    );

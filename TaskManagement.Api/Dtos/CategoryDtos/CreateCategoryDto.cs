using System.ComponentModel.DataAnnotations;
using Mapster;

namespace TaskManagement.Api.Dtos.CategoryDtos;
[GenerateMapper]
public record class CreateCategoryDto
(
        [Required][StringLength(50)] string Name,
        [StringLength(200)] string Description,
        int UserId


);
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Dtos.TaskDtos;

public record class UpdateTaskDto
(

[Required][StringLength(100)] String Title,
[Required][StringLength(100)] String Description,

[Required] DateOnly DueDate,
[Range(1, 3)] int Priority,
int UserId,
int CategoryId


);
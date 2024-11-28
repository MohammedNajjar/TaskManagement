using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace TaskManagement.Api.Dtos.TaskDtos;

public record class CreateTaskDto
(

[Required][StringLength(100)] String Title,
[Required][StringLength(100)] String Description,
[Required] DateTime DueDate,
bool IsCompleted,
[Range(1, 3)] int Priority,
int UserId,
int CategoryId,
string Status

);
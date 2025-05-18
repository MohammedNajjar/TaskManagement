using System.ComponentModel.DataAnnotations;
using Mapster;
using Microsoft.VisualBasic;

namespace TaskManagement.Api.Dtos.TaskDtos;
[GenerateMapper]
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
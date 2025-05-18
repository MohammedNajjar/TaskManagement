using System;
using Mapster;

namespace TaskManagement.Api.Entities;
[AdaptTo("[name]Dto"), GenerateMapper]
public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public  string? Password { get; set; }
}

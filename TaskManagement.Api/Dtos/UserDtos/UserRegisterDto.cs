using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Dtos.UserDtos;

public record class UserRegisterDto(
    [Required][StringLength(50)] String UserName,
    [Required][StringLength(100)] String Password
);

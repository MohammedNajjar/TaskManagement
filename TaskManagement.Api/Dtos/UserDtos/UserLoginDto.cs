using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Dtos.UserDtos;

public record class UserLoginDto(

    [Required][StringLength(50)] String Username,
    [Required][StringLength(100)] String Password

);


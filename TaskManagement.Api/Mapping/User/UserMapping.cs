using System;
using TaskManagement.Api.Dtos.UserDtos;
using TaskManagement.Api.Entities;


namespace TaskManagement.Api.Mapping.User;

public static class UserMapping
{
    public static Entities.User ToEntity(this UserRegisterDto user)
    {
        return new Entities.User()
        {
            UserName = user.UserName,
            Password = user.Password,

        };

    }
    public static UserProfileDto ToUserProfileDto(this Entities.User user)
    {
        return new(

            user.Id,
            user.UserName);

    }


}

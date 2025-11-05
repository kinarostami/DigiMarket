using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Infrastucture.Persistent.Ef;
using Shop.Query.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Users;

public static class UserMapper
{
    public static UserDto Map(this User user)
    {
        if (user == null)
            return null;

        return new UserDto()
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            AvatarImage = user.AvatarImage,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Family = user.Family,
            Name = user.Name,
            Gender = user.Gender,
            Password = user.Password,
            IsActive = user.IsAvtive,
            Roles = user.Roles.Select(x => new UserRoleDto()
            {
                Id = x.Id,
                RoleTitle = ""
            }).ToList()
        };
    }

    public async static Task<UserDto> SetUserRoleTitles(this UserDto userDto, ShopContext context)
    {
        var roleId = context.Roles.Select(x => x.Id);
        var result = await context.Roles.Where(x => roleId.Contains(x.Id)).ToListAsync();
        var roles = new List<UserRoleDto>();
        foreach (var item in result)
        {
            roles.Add(new UserRoleDto()
            {
                RoleId = item.Id,
                RoleTitle = item.Title, 
            });
        }

        userDto.Roles = roles;
        return userDto;
    }

    public static UserFilterData MapFilterData(this User user)
    {
        return new UserFilterData
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            Name = user.Name,
            Family = user.Family,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            Gender = user.Gender,
            AvatarImage = user.AvatarImage
        };
    }
}

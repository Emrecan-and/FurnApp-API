using System;
using System.Collections.Generic;
using System.Linq;
using FurnApp_API.Models;
using System.Threading.Tasks;
using FurnApp_API.DTO;

namespace FurnApp_API.Helper
{
    public static class DtoConverter
    {
        public static Users UserConverter(UserDTO dto)
        {
            Users user = new Users
            {
                UsersAddress = dto.UsersAddress,
                UsersAuthorization = dto.UsersAuthorization,
                UsersMail = dto.UsersMail,
                UsersPassword = dto.UsersPassword,
                UsersTelNo = dto.UsersTelNo
            };
            return user;
        }

    }
}

using FurnApp_API.Models;
using FurnApp_API.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, ApiResponse<Token>>
    {
        private readonly FurnAppContext db;
        private readonly IConfiguration configuration;

        public UserUpdateCommandHandler(FurnAppContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        public async Task<ApiResponse<Token>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await db.Users.FirstOrDefaultAsync(u => u.UsersMail == request.UserMail);
            if (existingUser == null)
            {
                return new ApiResponse<Token>
                {
                    Success = false,
                    Message = "User not found",
                    Data = null
                };
            }

            var userUpdate = request.UserUpdate;

            if (userUpdate.UsersPassword != null)
            {
                existingUser.UsersPassword = userUpdate.UsersPassword;
            }
            if (userUpdate.UsersAuthorization != null)
            {
                existingUser.UsersAuthorization = userUpdate.UsersAuthorization;
            }
            if (userUpdate.UsersTelNo != null)
            {
                existingUser.UsersTelNo = userUpdate.UsersTelNo;
            }
            if (userUpdate.UsersAddress != null)
            {
                existingUser.UsersAddress = userUpdate.UsersAddress;
            }

            await db.SaveChangesAsync();
            var token = TokenHandler.CreateToken(existingUser.UsersMail, configuration);

            return new ApiResponse<Token>
            {
                Success = true,
                Message = "User updated successfully",
                Data = token
            };
        }
    }
}
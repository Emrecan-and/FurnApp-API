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

namespace FurnApp_API.Med.Queries
{
    public class UserLogInQueryHandler : IRequestHandler<UserLogInQuery,ApiResponse<Token>>
    {
        private readonly FurnAppContext db;
        private readonly IConfiguration configuration;

        public UserLogInQueryHandler(FurnAppContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        public async Task<ApiResponse<Token>> Handle(UserLogInQuery request, CancellationToken cancellationToken)
        {
            if ( request.user.UsersMail == null || request.user.UsersPassword == null)
            {
                ApiResponse<Token> apiResponse = new ApiResponse<Token>()
                {
                    Data = null,
                    Message = "Required fields are empty!",
                    Success = false
                };
                return await Task.FromResult(apiResponse);
            }
            var user =await db.Users.FirstOrDefaultAsync(u => u.UsersMail == request.user.UsersMail && u.UsersPassword == request.user.UsersPassword);
            if (user == null)
            {
                ApiResponse<Token> apiResponse1 = new ApiResponse<Token>()
                {
                    Data = null,
                    Message = "Wrong email or password!",
                    Success = false
                };
                return await Task.FromResult(apiResponse1);
            }
            ApiResponse<Token> apiResponse2 = new ApiResponse<Token>()
            {
                Data = TokenHandler.CreateToken(request.user.UsersMail, configuration),
                Message = "Successfully LogIn!",
                Success = true
            };
            return await Task.FromResult(apiResponse2);
        }
    }
}

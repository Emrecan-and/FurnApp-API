using FurnApp_API.Helper;
using FurnApp_API.Models;
using FurnApp_API.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class UserSignUpCommandHandler : IRequestHandler<UserSignUpCommand,ApiResponse<Token>>
    {
        private readonly FurnAppContext db;
        private readonly IConfiguration configuration;

        public UserSignUpCommandHandler(FurnAppContext db,IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        async Task<ApiResponse<Token>> IRequestHandler<UserSignUpCommand, ApiResponse<Token>>.Handle(UserSignUpCommand request, CancellationToken cancellationToken)
        {

            if (request.user.UsersAddress == null || request.user.UsersAuthorization == null || request.user.UsersMail == null || request.user.UsersPassword == null ||
              request.user.UsersTelNo == null)
            {
                ApiResponse<Token> apiResponse = new ApiResponse<Token>() { Data = null, Message = "Required fields are empty!", Success = false };
                return await Task.FromResult(apiResponse);
            }
            else if (request.user.UsersPassword.Length < 6)
            {
                ApiResponse<Token> apiResponse = new ApiResponse<Token>() { Data = null, Message = "Password must contain at least 6 character!", Success = false };
                return await Task.FromResult(apiResponse);

            }
            else if (!(Validation.IsValidEmail(request.user.UsersMail)))
            {
                ApiResponse<Token> apiResponse = new ApiResponse<Token>() { Data = null, Message = "Wrong mail address!", Success = false };
                return await Task.FromResult(apiResponse);
            }
            else if (db.Users.Any(u => u.UsersMail == request.user.UsersMail))
            {
                ApiResponse<Token> apiResponse = new ApiResponse<Token>() { Data = null, Message = "This mail address was already taken!", Success = false };
                return await Task.FromResult(apiResponse);
            }

            var user1 = DtoConverter.UserConverter(request.user);
            db.Users.AddAsync(user1);
            db.SaveChangesAsync();
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

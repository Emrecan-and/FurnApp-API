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
    public class UserSignUpCommandHandler : IRequestHandler<UserSignUpCommand, Token>
    {
        private readonly FurnAppContext db;
        private readonly IConfiguration configuration;

        public UserSignUpCommandHandler(FurnAppContext db,IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }
        public Task<Token> Handle(UserSignUpCommand request, CancellationToken cancellationToken)
        {
            var user1 = DtoConverter.UserConverter(request.user);
            db.Users.AddAsync(user1);
            db.SaveChangesAsync();

            Token token = TokenHandler.CreateToken(user1.UsersMail, configuration);

            return Task.FromResult(token);
        }
    }
}

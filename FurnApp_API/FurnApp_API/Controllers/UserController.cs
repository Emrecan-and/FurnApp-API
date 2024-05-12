
using FurnApp_API.DTO;
using FurnApp_API.Helper;
using FurnApp_API.Med.Commands;
using FurnApp_API.Models;
using FurnApp_API.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly FurnAppContext db;
        private readonly IConfiguration _configuration;
        public UserController(IMediator mediator, FurnAppContext db,IConfiguration configuration)
        {
            this.mediator = mediator;
            this.db = db;
            this._configuration = configuration;
        }

        [HttpPost("SignUp")]
        public IActionResult SignUpUser(UserDTO user)
        {
            if (user.UsersAddress == null || user.UsersAuthorization == null || user.UsersMail == null || user.UsersPassword == null ||
                user.UsersTelNo == null)
            {
                return BadRequest("Cannot be null");
            }
            if (user.UsersPassword.Length < 6)
            {
                return BadRequest("Password must contain at least 6 character");
            }
            if (!(Validation.IsValidEmail(user.UsersMail)))
            {
                return BadRequest("Wrong email address");
            }
            if (db.Users.Any(u => u.UsersMail == user.UsersMail))
            {
                return BadRequest("This mail address was already taken");
            }
            var command = new UserSignUpCommand() { user = user };

            return Ok(mediator.Send(command));
        }


    }
}

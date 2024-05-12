
using FurnApp_API.DTO;
using FurnApp_API.Helper;
using FurnApp_API.Med.Commands;
using FurnApp_API.Med.Queries;
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
        public UserController(IMediator mediator, FurnAppContext db, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.db = db;
            _configuration = configuration;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUpUserAsync(UserDTO user)
        {
            var command = new UserSignUpCommand() { user = user };
            var response = await mediator.Send(command);
            if (response.Success)
            {
                return Ok(response);
               // MailSender mailSender = MailSender.GetInstance();
               // mailSender.LoginSender(user.UsersMail);
            }
            else
            {
                return BadRequest(response);
            }

        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInUserAsync(UserDTO user)
        {
            var query = new UserLogInQuery() { user = user };
            var response = await mediator.Send(query);
            if (response.Success)
            {
                
                return Ok(response);
                
            }
            else
            {
                return BadRequest(response);
            }
        }

    }
}

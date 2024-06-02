
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
    public class CartController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly FurnAppContext db;
        private readonly IConfiguration _configuration;
        public CartController(IMediator mediator, FurnAppContext db, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.db = db;
            _configuration = configuration;
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> CreateCart(int productId, string userMail)
        {
            var command = new CreateCartCommand { ProductId = productId, UserMail = userMail };
            var response = await mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("{UserMail}")]
        public async Task<IActionResult> GetCarts(string userMail)
        {
            var query = new GetCartQuery() { UserMail = userMail };
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

       [HttpDelete("DeleteCart")]
       public async Task<IActionResult> DeleteCart(int CartId)
        {
            var command = new DeleteCartCommand() { CartId = CartId };
            var response = await mediator.Send(command);
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

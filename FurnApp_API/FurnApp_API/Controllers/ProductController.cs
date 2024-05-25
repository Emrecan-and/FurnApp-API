
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
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly FurnAppContext db;
        private readonly IConfiguration _configuration;
        public ProductController(IMediator mediator, FurnAppContext db, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.db = db;
            _configuration = configuration;
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> CreateCart(int productId, int userId)
        {
            var command = new CreateCartCommand { ProductId = productId, UserId = userId };
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCarts(int userId)
        {
            var query = new GetCartQuery() { UserId = userId };
            var response = await mediator.Send(query);
            return Ok(response);

            /*  if (response.Success)
              {
                  return Ok(response);
              }
              else
              {
                  return BadRequest(response);
              }*/
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

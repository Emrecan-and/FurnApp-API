
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
    public class ProductColorController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly FurnAppContext db;
        private readonly IConfiguration _configuration;
        public ProductColorController(IMediator mediator, FurnAppContext db, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.db = db;
            _configuration = configuration;
        }

        [HttpPost("AddProductColor")]
        public async Task<IActionResult> CreateProductColor(int productId, int colorId)
        {
            var command = new CreateProductColorCommand { productId = productId, colorId = colorId };
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
        
        [HttpPut("UpdateProductColor")]
        public async Task<IActionResult> UpdateProductColor(int colorId, int productId)
        {
            var command = new UpdateProductColorCommand() { ProductId = productId, ColorId = colorId };
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

        [HttpDelete("DeleteProductColor")]
        public async Task<IActionResult> DeleteProductColor(int colorId, int productId)
        {
            var command = new DeleteProductColorCommand() { colorId = colorId, productId = productId };
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

        [HttpGet("GetProductColors")]
        public async Task<IActionResult> GetProductColors()
        {
            var command = new GetProductColorsQuery();
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

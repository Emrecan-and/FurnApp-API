
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
    public class ColorController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly FurnAppContext db;
        private readonly IConfiguration _configuration;
        public ColorController(IMediator mediator, FurnAppContext db, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.db = db;
            _configuration = configuration;
        }

      

        [HttpPost("AddColor")]
        public async Task<IActionResult> CreateColor(string colorName)
        {
            var command = new CreateColorCommand { ColorName = colorName };
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

        [HttpDelete("DeleteColor")]
        public async Task<IActionResult> DeleteColor(int ColorId)
        {
            var command = new DeleteColorCommand() { ColorId = ColorId };
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
        [HttpPut("UpdateColor")]
        public async Task<IActionResult> UpdateColor(int colorId, string NewColorName)
        {
            var command = new UpdateColorCommand() { NewColorName = NewColorName, ColorId = colorId };
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

        [HttpGet("GetColors")]
        public async Task<IActionResult> GetColors()
        {
            var command = new GetColorsQuery();
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

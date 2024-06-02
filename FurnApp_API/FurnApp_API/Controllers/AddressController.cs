
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
    public class AddressController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly FurnAppContext db;
        private readonly IConfiguration _configuration;
        public AddressController(IMediator mediator, FurnAppContext db, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.db = db;
            _configuration = configuration;
        }

        [HttpPost("CreateAddress")]
        public async Task<IActionResult> CreateAddress(AddressDTO2 address)
        {
            var command = new CreateAddressCommand() {
            BuildingNumber=address.BuildingNumber,
            City=address.City,
            HomeNumber=address.HomeNumber,
            District=address.District,
            Street=address.Street,
            PostalCode=address.PostalCode,
            Neighborhood=address.Neighborhood
            };
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

        [HttpGet("GetAddresses")]
        public async Task<IActionResult> GetAddress()
        {
            var query = new GetAddressesQuery();
            var response =await mediator.Send(query);
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

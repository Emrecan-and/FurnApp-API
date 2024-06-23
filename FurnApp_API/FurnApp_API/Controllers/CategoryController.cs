using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FurnApp_API.Commands;
using MediatR;
using FurnApp_API.Models;
using FurnApp_API.Med.Commands;
using Microsoft.Extensions.Configuration;
using FurnApp_API.Security;
using FurnApp_API.Med.Queries;

namespace FurnApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly FurnAppContext _db;
        private readonly IConfiguration _configuration;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryCommand(string CategoryName)
        {
            var command = new CreateCategoryCommand() { CategoryName = CategoryName };
            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryCommand(int id, string CategoryName)
        {
            var command = new UpdateCategoryCommand { CategoryId = id, CategoryName = CategoryName };
            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryCommand(int id)
        {
            var command = new DeleteCategoryCommand { CategoryId = id };
            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var command = new GetCategoryQuery();
            var response = await _mediator.Send(command);
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
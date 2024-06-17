using FurnApp_API.Med.Commands;
using FurnApp_API.Med.Queries;
using FurnApp_API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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

      
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] List<int> categoryIds, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] string sortBy)
        {
            var query = new GetProductsQuery
            {
                categoryIds = categoryIds,
                minPrice = minPrice,
                maxPrice = maxPrice,
                sortBy = sortBy
            };

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

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(string ProductName, int? ProductStock, decimal? ProductPrice, string ProductDescription, int? CategoryId)
        {
            var command = new CreateProductCommand()
            {
                CategoryId=CategoryId,
                ProductDescription=ProductDescription,
                ProductStock=ProductStock,
                ProductName=ProductName,
                ProductPrice=ProductPrice
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
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int ProductId,string ProductName, int? ProductStock, decimal? ProductPrice, string ProductDescription, int? CategoryId)
        {
            var command = new UpdateProductCommand()
            {
                ProductId=ProductId,
                CategoryId = CategoryId,
                ProductDescription = ProductDescription,
                ProductStock = ProductStock,
                ProductName = ProductName,
                ProductPrice = ProductPrice
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
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int ProductId)
        {
            var command = new DeleteProductCommand()
            {
                ProductId = ProductId
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
        [HttpGet("GetEightProducts")]
        public async Task<IActionResult> GetEightProducts()
        {
            var query = new GetEightProductsQuery();
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
        [HttpGet("SearchProducts")]
        public async Task<IActionResult> GetSearchProducts(string ProductName)
        {
            var query = new GetSearchProductQuery() { ProductName = ProductName };
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

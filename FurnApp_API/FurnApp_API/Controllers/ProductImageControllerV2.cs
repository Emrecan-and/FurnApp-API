using System.Threading.Tasks;
using FurnApp_API.DTO;
using FurnApp_API.Med.Commands;
using FurnApp_API.Med.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnApp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductImageV2Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductImageV2Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("UploadImages")]
        public async Task<IActionResult> MultiUploadImage(IFormFileCollection fileCollection, int productId)
        {
            var command = new CreateProductImageCommand { FileCollection = fileCollection, ProductId = productId };
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("DeleteImages")]
        public async Task<IActionResult> DeleteImages(int productId)
        {
            var command = new DeleteProductImageCommand() { ProductId = productId };
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteImage(SingleProductImageDto image)
        {
            var command = new DeleteSingleProductImageCommand() { ProductId = image.ProductId, Base64 = image.Base64 };
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("GetImages")]
        public async Task<IActionResult> GetImages(int productId)
        {
            var command = new GetProductImagesQuery() { ProductId = productId };
            var response = await _mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
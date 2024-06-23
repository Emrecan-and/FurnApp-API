using FurnApp_API.DTO;
using FurnApp_API.Med.Commands;
using FurnApp_API.Med.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FurnApp_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrdersDTO2 order)
        {
            var command = new CreateOrdersCommand
            {
                OrderDate = order.OrderDate,
                CargoNo = order.CargoNo,
                UsersId = order.UsersId,
                ProductId = order.ProductId,
                AddressId = order.AddressId
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
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrdersCommand { OrderId = id };
            var response = await mediator.Send(command);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);//örnek
            }
        }
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var query = new GetOrdersQuery();
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

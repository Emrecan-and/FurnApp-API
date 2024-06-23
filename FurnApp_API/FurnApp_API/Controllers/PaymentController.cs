using FurnApp_API.DTO;
using FurnApp_API.Med.Commands;
using FurnApp_API.Med.Queries;
using FurnApp_API.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FurnApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly FurnAppContext db;
        private readonly IConfiguration _configuration;
        public PaymentController(IMediator mediator, FurnAppContext db, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.db = db;
            _configuration = configuration;
        }


        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(PaymentDTO2 payment, int productId, int quantity)
        {
            var command = new CreatePaymentCommand
            {
                CreditCardNo = payment.CreditCardNo,
                CardName = payment.CardName,
                CardMonth = payment.CardMonth,
                CardYear = payment.CardYear,
                CardCvv = payment.CardCvv,
                CargoPrice = payment.CargoPrice,
                UsersId = payment.UsersId,
                CargoCompany = payment.CargoCompany,
                ProductId = productId,
                Quantity = quantity
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


        [HttpGet("GetPayments")]
        public async Task<IActionResult> GetPayments()
        {
            var query = new GetPaymentsQuery();
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

        [HttpDelete("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var command = new DeletePaymentCommand { PaymentId = id };
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
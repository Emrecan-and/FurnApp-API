using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using System;

namespace FurnApp_API.Med.Commands
{
    public class CreateOrdersCommand : IRequest<ApiResponse<OrdersDTO2>>
    {
        public DateTime OrderDate { get; set; }
        public int? CargoNo { get; set; }
        public int? UsersId { get; set; }
        public int? ProductId { get; set; }
        public int? AddressId { get; set; }
    }
}
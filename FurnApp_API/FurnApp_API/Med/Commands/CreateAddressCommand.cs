using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class CreateAddressCommand : IRequest<ApiResponse<AddressDTO2>>
    {
        public string Neighborhood { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string HomeNumber { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}

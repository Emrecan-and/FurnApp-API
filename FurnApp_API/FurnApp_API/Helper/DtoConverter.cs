using System;
using System.Collections.Generic;
using System.Linq;
using FurnApp_API.Models;
using System.Threading.Tasks;
using FurnApp_API.DTO;

namespace FurnApp_API.Helper
{
    public static class DtoConverter
    {
        public static Users UserConverter(UserDTO dto)
        {
            Users user = new Users
            {
                UsersAddress = dto.UsersAddress,
                UsersAuthorization = dto.UsersAuthorization,
                UsersMail = dto.UsersMail,
                UsersPassword = dto.UsersPassword,
                UsersTelNo = dto.UsersTelNo
            };
            return user;
        }

        public static Address AddressConverter(AddressDTO2 dto)
        {
            Address address = new Address
            {
                BuildingNumber = dto.BuildingNumber,
                City = dto.City,
                PostalCode = dto.PostalCode,
                Street = dto.Street,
                District = dto.District,
                HomeNumber = dto.HomeNumber,
                Neighborhood = dto.Neighborhood
            };
            return address;
        }
        public static Orders OrdersConverter(OrdersDTO2 dto)
        {
            Orders order = new Orders
            {
                OrderDate = dto.OrderDate,
                CargoNo = dto.CargoNo,
                UsersId = dto.UsersId,
                ProductId = dto.ProductId,
                AddressId = dto.AddressId
            };
            return order;
        }
        public static Payment PaymentConverter(PaymentDTO2 dto)
        {
            Payment payment = new Payment
            {
                CreditCardNo = dto.CreditCardNo,
                CardName = dto.CardName,
                CardMonth = dto.CardMonth,
                CardYear = dto.CardYear,
                CardCvv = dto.CardCvv,
                CargoPrice = dto.CargoPrice,
                UsersId = dto.UsersId,
                CargoCompany = dto.CargoCompany
            };
            return payment;
        }
    }
}

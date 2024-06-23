using FurnApp_API.DTO;
using FurnApp_API.Models;
using MediatR;

public class CreatePaymentCommand : IRequest<ApiResponse<decimal>>
{
    public int CreditCardNo { get; set; }
    public string CardName { get; set; }
    public int CardMonth { get; set; }
    public int CardYear { get; set; }
    public int CardCvv { get; set; }
    public decimal? CargoPrice { get; set; }
    public int? UsersId { get; set; }
    public string CargoCompany { get; set; }
    public int ProductId { get; set; }  // Yeni alan
    public int Quantity { get; set; }  // Yeni alan
}
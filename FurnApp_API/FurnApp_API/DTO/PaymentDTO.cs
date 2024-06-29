namespace FurnApp_API.DTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int CreditCardNo { get; set; }
        public string CardName { get; set; }
        public int CardMonth { get; set; }
        public int CardYear { get; set; }
        public int CardCvv { get; set; }
        public decimal? CargoPrice { get; set; }
        public int? UsersId { get; set; }
        public string UserFullName { get; set; }

        public string CargoCompany { get; set; }
    }

    public class PaymentDTO2
    {
        public int CreditCardNo { get; set; }
        public string CardName { get; set; }
        public int CardMonth { get; set; }
        public int CardYear { get; set; }
        public int CardCvv { get; set; }
        public decimal? CargoPrice { get; set; }
        public int? UsersId { get; set; }
        public string CargoCompany { get; set; }
    }
}
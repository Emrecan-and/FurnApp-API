using System;
using System.Collections.Generic;

namespace FurnApp_API.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int CreditCardNo { get; set; }
        public string CardName { get; set; }
        public int CardMonth { get; set; }
        public int CardYear { get; set; }
        public int CardCvv { get; set; }
        public decimal? CargoPrice { get; set; }
        public int? UsersId { get; set; }
        public string CargoCompany { get; set; }

        public virtual Users Users { get; set; }
    }
}

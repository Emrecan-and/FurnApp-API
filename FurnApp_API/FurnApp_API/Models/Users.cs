using System;
using System.Collections.Generic;

namespace FurnApp_API.Models
{
    public partial class Users
    {
        public Users()
        {
            Cart = new HashSet<Cart>();
            Orders = new HashSet<Orders>();
            Payment = new HashSet<Payment>();
        }

        public int UsersId { get; set; }
        public string UsersMail { get; set; }
        public string UsersPassword { get; set; }
        public int? UsersAuthorization { get; set; }
        public int? UsersTelNo { get; set; }
        public string UsersAddress { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnApp_API.DTO
{
    public class CartDTO
    {
        public int cartId  { get; set; }

        public int userId { get; set; }

        public int productId { get; set; }
    }
}

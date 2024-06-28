using System.Collections.Generic;

namespace FurnApp_API.DTO
{
    public class ProductImageDTO
    {
        public int ProductId { get; set; }
        public List<string> Base64Images { get; set; }
    }
}
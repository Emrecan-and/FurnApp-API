namespace FurnApp_API.Models
{
    public partial class ProductImage
    {
        public int ProductId { get; set; }
        public string Base64 { get; set; }
        
        public virtual Products Product { get; set; }
    }
}
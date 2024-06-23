using FurnApp_API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FurnApp_API.Med.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResponse<Categories>>
    {
        private readonly FurnAppContext _db;

        public DeleteCategoryCommandHandler(FurnAppContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse<Categories>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _db.Categories
                .Include(c => c.Products)
                .ThenInclude(p => p.Cart)
                .Include(c => c.Products)
                .ThenInclude(p => p.Orders)
                .Include(c => c.Products)
                .ThenInclude(p => p.ProductColors)
                .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId);

            if (category == null)
            {
                return new ApiResponse<Categories>
                {
                    Data = null,
                    Message = "Category not found!",
                    Success = false
                };
            }

            // İlgili kategorideki ürünleri ve ilişkili kayıtları sil
            foreach (var product in category.Products)
            {
                // Cart kayıtlarını sil
                var cartsToRemove = _db.Cart.Where(c => c.ProductId == product.ProductId);
                _db.Cart.RemoveRange(cartsToRemove);

                // Order kayıtlarını sil
                var ordersToRemove = _db.Orders.Where(o => o.ProductId == product.ProductId);
                _db.Orders.RemoveRange(ordersToRemove);

                // ProductColor kayıtlarını sil
                var productColorsToRemove = _db.ProductColors.Where(pc => pc.ProductId == product.ProductId);
                _db.ProductColors.RemoveRange(productColorsToRemove);

                // Ürünü sil
                _db.Products.Remove(product);
            }

            // Kategoriyi sil
            _db.Categories.Remove(category);

            await _db.SaveChangesAsync();

            return new ApiResponse<Categories>
            {
                Data = category,
                Message = "Category and related products were deleted!",
                Success = true
            };
        }
    }
}
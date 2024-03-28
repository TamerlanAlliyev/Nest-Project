using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Data;
using Nest.Models;
using Nest.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly NestContext _context;

        public ProductViewComponent(NestContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
        {
            IQueryable<Product> productsQuery = _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.CategoryProducts)
                    .ThenInclude(p => p.Category!)
                .Include(p => p.Vendor)
                .Include(p => p.CustomerRatings)
                .Include(p => p.ProductImages);

            if (categoryId != null)
            {
                productsQuery = productsQuery.Where(p => p.CategoryProducts!.Any(cp => cp.CategoryId == categoryId));
            }

            List<Product> products = await productsQuery
                .OrderByDescending(p => p.Id)
                .Take(20)
                .ToListAsync();

            ProductVM productVM = new ProductVM()
            {
                Products = products
            };

            return View(productVM);
        }
    }
}

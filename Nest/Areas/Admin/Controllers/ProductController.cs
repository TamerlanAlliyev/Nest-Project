using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Areas.Admin.ViewModels;
using Nest.Data;

namespace Nest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly NestContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(NestContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages.Where(pi => pi.IsMain == true)) 
                .Include(p => p.CustomerRatings)
                .Include(p => p.Vendor)
                .Include(p => p.SizeWeights)
                .Where(p => !p.IsDeleted)
                .ToListAsync();

            var sizeWeights = await _context.SizeWeights.Where(sw => !sw.IsDeleted).ToListAsync();
            var customerRatings = await _context.CustomerRatings.Where(cr => !cr.IsDeleted).ToListAsync();

            var productVM = new ProductVM
            {
                Products = products,
                SizeWeights = sizeWeights,
                CustomerRatings = customerRatings
            };

            return View(productVM);
        }
    }
}

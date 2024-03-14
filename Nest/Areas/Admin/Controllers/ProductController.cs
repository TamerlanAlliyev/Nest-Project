using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Areas.Admin.ViewModels;
using Nest.Data;
using Nest.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

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
                .Include(p => p.Vendor)
                .Where(p => !p.IsDeleted)
                .AsNoTracking()
                .ToListAsync();


            var productVM = new ProductVM
            {
                Products = products,
            };

            return View(productVM);
        }



        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                         .Include(p => p.Category)
                         .Include(p => p.Vendor)
                         .Include(p => p.CustomerRatings)
                             .ThenInclude(p => p.Customer)
                         .Include(p => p.ProductSizes)
                             .ThenInclude(p => p.Size)
                         .Include(p => p.ProductWeights)
                             .ThenInclude(p => p.Weight)
                         .Include(p => p.ProductImages)
                         .FirstOrDefaultAsync();



            return View(product);


        }


    }
}

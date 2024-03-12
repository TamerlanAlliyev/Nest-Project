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
                           .Where(p => !p.IsDeleted)
                           .Include(p => p.Category)
                           .Include(p => p.ProductImages)
                           .Include(p => p.Vendor)
                           .AsNoTracking()
                           .FirstOrDefaultAsync();


            return View(product);


        }





















        #region 2
        //public async Task<IActionResult> Details(int id)
        //{
        //    var product = await _context.Products
        //                   .Where(p => !p.IsDeleted)
        //                   .Include(p => p.Category)
        //                   .Include(p => p.ProductImages)
        //                   .Include(p => p.Vendor)
        //                   .AsNoTracking()
        //                   .Select( p => new
        //                   {
        //                       ProductId = p.Id,
        //                       ProductName = p.Name,
        //                       ProductDescription = p.Description,
        //                       ProductSellPrice = p.SellPrice,
        //                       ProductDiscountPrice = p.DiscountPrice,
        //                       ProductVendor = p.Vendor.FullName,
        //                       ProductCreateBy = p.CreateBy,
        //                       ProductCreated = p.Created,
        //                       ProductModifiedBy = p.ModifiedBy,
        //                       ProductModified = p.Modified,
        //                       ProductIPAddress = p.IPAddress,

        //                       CategoryNames = p.Category.Where(pi => !pi.IsDeleted).Select(c => c.Name).ToList(),

        //                       ProductImages = p.ProductImages.Where(pi => !pi.IsDeleted).Select(pi => pi.Url).ToList(),
        //                       ProductIsMain = p.ProductImages.Where(pi => !pi.IsDeleted && pi.IsMain == true).Select(pi => pi.Url).FirstOrDefault(),

        //                       CustomerRatings = p.CustomerRatings.Where(cr => !cr.IsDeleted).Select(cr => new 
        //                       {
        //                           CustomerRating = cr.Evaluation,
        //                           CustomerComment = cr.Comment,
        //                           CustomerName = cr.Customer.FullName
        //                       }),

        //                       ProductSizes = p.Sizes.Where(s => !s.IsDeleted && s.Count > 0).Select(s => new
        //                       {
        //                           SizeName = s.Name,
        //                           SizeCount = s.Count,
        //                       }),

        //                       ProductWeights = p.Weights.Where(w => !w.IsDeleted && w.Count > 0).Select(w => new
        //                       {
        //                           WeightGram = w.Gram,
        //                           WeightCount = w.Count
        //                       })

        //                   })
        //                   .FirstOrDefaultAsync();
        //    //if (product == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return Json(product);


        //}
        #endregion







        #region 1

        //public async Task<IActionResult> Details(int id)
        //{
        //    var product = await _context.Products
        //        .Where(p => p.Id == id && !p.IsDeleted)
        //        .Include(p => p.Category)
        //        .Include(p => p.ProductImages)
        //        .Include(p => p.Vendor)
        //        .Include(p => p.CustomerRatings) // Include CustomerRatings
        //        .Include(p => p.Sizes) // Include Sizes
        //        .Include(p => p.Weights) // Include Weights
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync();

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    var productVM = new ProductVM
        //    {
        //        Products = new List<Product> { product }, // Add product to Products list

        //        CustomerRatings = product.CustomerRatings.Where(cr => !cr.IsDeleted).ToList(),
        //        Size = product.Sizes.Where(s => !s.IsDeleted).ToList(), // Assuming you want a list of Size objects
        //        Weights = product.Weights.Where(w => !w.IsDeleted).ToList() // Assuming you want a list of Weight objects
        //    };

        //    return View(productVM);
        //}


        #endregion




















    }
}

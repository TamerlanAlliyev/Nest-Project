using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Areas.Admin.ViewModels;
using Nest.Data;
using Nest.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

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




        //public async Task<IActionResult> Create()
        //{
        //    //await _context.Categories.ToListAsync();
        //    ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        //    ViewBag.Sizes = new SelectList(_context.Sizes, "Id", "Name");
        //    ViewBag.Weights = new SelectList(_context.Weights, "Id", "Gram");
        //    ViewBag.Vendors = new SelectList(_context.Vendors, "Id", "FullName");

        //    //List<Category> Category = await _context.Categories.ToListAsync();
        //    //List<Size> Sizes = await _context.Sizes.ToListAsync();
        //    //List<Weight> Weights = await _context.Weights.ToListAsync();
        //    //List<Vendor> Vendors = await _context.Vendors.ToListAsync();
        //    //ProductVM productVM = new ProductVM()
        //    //{
        //    //    Categories = Category,
        //    //    Sizes = Sizes,
        //    //    Weights = Weights,
        //    //    Vendors = Vendors
        //    //};



        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(ProductVM product)
        //{
        //    if (product == null)
        //    {
        //        return BadRequest();
        //    }


        //    string uploadFolder = Path.Combine(_environment.WebRootPath, "cilent", "icons", "categories");
        //    //string uniqueFileName = await category.FormFile.SaveToAsync(uploadFolder);

        //    var IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        //    var createBy = 1;
        //    var created = DateTime.UtcNow;

        //    var newCategory = new Category
        //    {
        //        //Name = category.Name.Trim(),
        //        //Icon = uniqueFileName,
        //        IPAddress = IpAddress,
        //        CreateBy = createBy,
        //        Created = created,
        //        IsDeleted = false,
        //    };

        //    await _context.Categories.AddAsync(newCategory);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}































        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            //ViewBag.Sizes = new SelectList(_context.Sizes, "Id", "Name");
            ViewBag.Sizes = await _context.Sizes.Where(s=>!s.IsDeleted).ToListAsync();
            ViewBag.Weights = new SelectList(_context.Weights, "Id", "Gram");
            ViewBag.Vendors = new SelectList(_context.Vendors, "Id", "FullName");
            ProductVM productVM = new ProductVM
            {
                Sizes =await _context.Sizes.Where(s => !s.IsDeleted).ToListAsync()
            };
            return View(productVM);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductVM product)
        {
            var _product = new ProductVM {
             ProductSizes =    
            {

            },
                Product = new Product
                {
                    Name = product.Product.Name,
                    Description = product.Product.Description,
                    SellPrice = product.Product.SellPrice,
                    DiscountPrice = product.Product.DiscountPrice,
                    Files = product.Product.Files,
                    IsMainFile = product.Product.IsMainFile,
                    IsHoverFile = product.Product.IsHoverFile,
                    IsDeleted = false,
                    CreateBy = 1,
                    Created = DateTime.UtcNow,
                    IPAddress = product.Product.IPAddress,

                    Category = product.Product.Category,
                    VendorId = product.Product.VendorId,
                }
            };
            return View();
        }
    }
}


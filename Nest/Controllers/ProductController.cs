using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nest.Data;
using Nest.Models;
using Nest.ViewModels;
using Newtonsoft.Json;

namespace Nest.Controllers
{
    public class ProductController : Controller
    {
        public readonly NestContext _context;

        public ProductController(NestContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PreloaderPartialView = true;
            ViewBag.MobileHeaderPartialView = true;
            ViewBag.QuickPartialView = true;


            List<Product> products = await _context.Products.Where(p => !p.IsDeleted)
                                                            .Include(p => p.CategoryProducts)//
                                                            .ThenInclude(p=>p.Category)//
                                                            .Include(p => p.Vendor)
                                                            .Include(p => p.CustomerRatings)
                                                            .OrderByDescending(p => p.Id).Take(20)
                                                            .Include(p => p.ProductImages)
                                                            .ToListAsync();

            List<CustomerRating> ratingsForProduct = await _context.CustomerRatings
                                                            .Where(cr => cr.ProductId == cr.Product.Id && !cr.IsDeleted)
                                                            .ToListAsync();

            List<Category> categories = await _context.Categories
                                              .Where(c => !c.IsDeleted)
                                              .Include(c => c.CategoryProducts)
                                              .ToListAsync();

            List<ProductImage> productImages = await _context.ProductImages.Where(pi => !pi.IsDeleted)
                                                                           .Include(pi => pi.Product)
                                                                           .ToListAsync();

            ProductVM productVM = new ProductVM()
            {
                Products = products,
                CustomerRatings = ratingsForProduct,
                Categories = categories
            };

            return View(productVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.QuickPartialView = true;
            ViewBag.MobileHeaderPartialView = true;
            ViewBag.PreloaderPartialView = true;

            if (id < 0)
            {
                return BadRequest();
            }



            Product? product = await _context.Products.Where(p => !p.IsDeleted && p.Id==id)
                                                                        .Include(p => p.CategoryProducts)
                                                                        .ThenInclude(p=>p.Category)
                                                                        .Include(p => p.Vendor)
                                                                        .Include(p => p.CustomerRatings)
                                                                        .Include(p => p.ProductSizes)
                                                                            .ThenInclude(p => p.Size)
                                                                        .Include(p => p.ProductWeights)
                                                                            .ThenInclude(p => p.Weight)
                                                                        .Include(p => p.ProductImages)
                                                                        .FirstOrDefaultAsync();

            List<Category> categories = await _context.Categories
                                  .Where(c => !c.IsDeleted)
                                  .Include(c => c.CategoryProducts)
                                  .ThenInclude(p=>p.Product)
                                  .ToListAsync();

            ProductVM productVM = new ProductVM()
            {
                Product = product,
                Categories = categories
            };

            //TempData["ProductImage"] = product;



            return View(productVM);
        }



        public async Task<IActionResult> AddToCart(int id)
        {
            var existProduct = await _context.Products.AnyAsync(x => x.Id == id);
            if (!existProduct) return NotFound();

            List<BasketVM> BasketVM = GetBasket();
            BasketVM cartVm = BasketVM.Find(x => x.Id == id);
            if (cartVm != null)
            {
                cartVm.Count++;
            }
            else
            {
                BasketVM.Add(new BasketVM
                {
                    Count = 1,
                    Id = id
                });
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(BasketVM));
            return RedirectToAction(nameof(Index));
        }
        private List<BasketVM> GetBasket()
        {
            List<BasketVM> BasketVM;
            if (Request.Cookies["basket"] != null)
            {
                BasketVM = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else BasketVM = new List<BasketVM>();
            return BasketVM;
        }
    }
}

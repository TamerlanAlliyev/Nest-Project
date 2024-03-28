using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Areas.Admin.ViewModels;
using Nest.Data;
using Nest.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using YourNamespace.Extensions;

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
                .Include(p => p.CategoryProducts)
                .ThenInclude(p => p.Category)
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
            var product = await _context.Products.Where(p=>!p.IsDeleted&&p.Id==id)
                         .Include(p => p.CategoryProducts)
                         .ThenInclude(p => p.Category)
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



        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            var vendors = await _context.Vendors.Where(c => !c.IsDeleted).ToListAsync();
            //var size = await _context.Sizes.Where(s => !s.IsDeleted).ToListAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Vendor = new SelectList(vendors, "Id", "FullName");
            ViewBag.Sizes = await _context.Sizes.Where(s => !s.IsDeleted).ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM productVM)
        {
            var categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            var vendor = await _context.Vendors.Where(c => !c.IsDeleted).ToListAsync();
            //var size = await _context.Sizes.Where(s => !s.IsDeleted).ToListAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Vendor = new SelectList(vendor, "Id", "FullName");
            //ViewBag.Sizes = new SelectList(size, "Id", "Name");
            ViewBag.Sizes = await _context.Sizes.Where(s => !s.IsDeleted).ToListAsync();


            var product = new Product
            {
                Name = productVM.Name,
                Description = productVM.Description,
                SellPrice = productVM.Price,
                DiscountPrice = productVM.DiscountPrice,
                VendorId = productVM.VendorId,
                IsDeleted = false,
                CreateBy = 1,
                Created = DateTime.UtcNow,
                IPAddress = "1"
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();



            var categoryProduct = new CategoryProduct
            {
                ProductId = product.Id,
                CategoryId = productVM.CategoryId
            };


            List<ProductSize> sizes = new List<ProductSize>();

            if (productVM.SizeId != null && productVM.SizeCount != null )
            {
                for (int i = 0; i < productVM.SizeId.Count; i++)
                {
                    sizes.Add(new ProductSize
                    {
                        ProductId = product.Id,
                        SizeId = productVM.SizeId[i],
                        Count = productVM.SizeCount[i]
                    });
                }
            }















            List<ProductImage> image = new List<ProductImage>();

            if (productVM.Files != null)
            {
                foreach (var files in productVM.Files)
                {
                    if (!files.FileSize(2))
                    {
                        ModelState.AddModelError("Files", "Files cannot be more than 2mb");
                        return View(productVM);
                    }

                    if (!files.FileTypeAsync("image"))
                    {
                        ModelState.AddModelError("Files", "Files must be image type!");
                        return View(productVM);
                    }

                    var path = Path.Combine(_environment.WebRootPath, "cilent", "imgs", "products");
                    var fileName = await files.SaveToAsync(path);

                    var productImage = CreatImage(fileName,false,false,product);
                    image.Add(productImage);
                    //await _context.ProductImages.AddAsync(fileName);
                }
            }


            if (productVM.MainFile!=null)
            {
                if (!productVM.MainFile.FileSize(2))
                {
                    ModelState.AddModelError("Files", "Files cannot be more than 2mb");
                    return View(productVM);

                }

                if (!productVM.MainFile.FileTypeAsync(""))
                {
                    ModelState.AddModelError("Files", "Files must be image type!");
                    return View(productVM);
                }
                var path = Path.Combine(_environment.WebRootPath, "cilent", "imgs", "products");
                var fileName = await productVM.MainFile.SaveToAsync(path);


                var productImage = CreatImage(fileName, true, false, product);
                image.Add(productImage);
            }

            if (productVM.HoverFile != null)
            {
                if (!productVM.HoverFile.FileSize(2))
                {
                    ModelState.AddModelError("Files", "Files cannot be more than 2mb");
                    return View(productVM);

                }

                if (!productVM.HoverFile.FileTypeAsync(""))
                {
                    ModelState.AddModelError("Files", "Files must be image type!");
                    return View(productVM);
                }
                var path = Path.Combine(_environment.WebRootPath, "cilent", "imgs", "products");
                var fileName = await productVM.HoverFile.SaveToAsync(path);


                var productImage = CreatImage(fileName, false, true, product);
                image.Add(productImage);
            }


            await _context.CategoryProduct.AddAsync(categoryProduct);
            await _context.ProductImages.AddRangeAsync(image);
            await _context.ProductSize.AddRangeAsync(sizes);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



        }




        public ProductImage CreatImage(string Url,bool Main,bool Hover,Product product)
        {
            return new ProductImage
            {
                Url=Url,
                IsMain=Main,
                IsHover=Hover,
                Product=product,
                IsDeleted=false,
                CreateBy = 1,
                Created = DateTime.UtcNow,
                IPAddress="1"
                
            };
        }





        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefaultAsync();
            product.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Data;
using Nest.Models;
using YourNamespace.Extensions;

namespace Nest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public readonly NestContext _context;
        private readonly IWebHostEnvironment _environment;
        public CategoryController(NestContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Categories.Where(c => !c.IsDeleted).AsNoTracking().ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var list = await _context.Categories.Include(c => c.Product).Where(c => !c.IsDeleted && c.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }

        //public IActionResult Delete(int id)
        //{
        //    var category = _context.Categories.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefault();
        //    category.IsDeleted = true;
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();

            var category = _context.Categories.Where(c => c.Id == id && !c.IsDeleted).FirstOrDefault();

            if (category == null) return NotFound();

            category.IsDeleted = true;
            _context.SaveChanges();

            return Ok(new { success = true, message = "Category deleted successfully." });
        }


        public async Task<IActionResult> DeletedList()
        {
            var category = await _context.Categories.Where(c => c.IsDeleted).ToListAsync();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeletedListReturn(int id)
        {
            var category = await _context.Categories.Where(c => c.IsDeleted && c.Id == id).FirstOrDefaultAsync();
            category.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction(nameof(DeletedList));
        }



        //[HttpPost]
        //public async Task<IActionResult> HardDeleted(int id)
        //{
        //    var category = await _context.Categories.FindAsync(id);
        //    if (category != null)
        //    {
        //        string path = Path.Combine(_environment.WebRootPath, "admin", "icons", "categories",category.Icon);
        //        if (System.IO.File.Exists(path))
        //        {
        //            System.IO.File.Delete(path);
        //        }
        //        _context.Categories.Remove(category);
        //        await _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction(nameof(DeletedList));
        //}

        [HttpPost]
        public async Task<IActionResult> HardDeleted(int id)
        {
            if (id < 1) return BadRequest();

            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                string path = Path.Combine(_environment.WebRootPath, "admin", "icons", "categories", category.Icon);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return Ok(new { success = true, message = "Category deleted successfully." });
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }


            if (category.FormFile == null)
            {
                ModelState.AddModelError("FormFile", "Please select an image.");
                return View(category);
            }

            if (!category.FormFile.FileTypeAsync("image/"))
            {
                ModelState.AddModelError("FormFile", "Wrong file type");
                return View(category);
            }

            if (category.FormFile.FileSize(2))
            {
                ModelState.AddModelError("FormFile", "File max size is 2mb");
                return View(category);
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ipAddress == null)
            {
                return BadRequest("IP address is not available.");
            }


            string uploadFolder = Path.Combine(_environment.WebRootPath, "admin", "icons", "categories");
            string uniqueFileName = await category.FormFile.SaveToAsync(uploadFolder);

            var IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            var createBy = 1;
            var created = DateTime.UtcNow;

            var newCategory = new Category
            {
                Name = category.Name.Trim(),
                Icon = uniqueFileName,
                IPAddress = IpAddress,
                CreateBy = createBy,
                Created = created,
                IsDeleted = false,
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            if (id < 1) return BadRequest();
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null) return NotFound();


            return View(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id < 1)
                return BadRequest();

            var entity = await _context.Categories.FindAsync(id);

            if (entity == null)
                return NotFound();

            var IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

            if (category.Name == null)
            {
                if (!ModelState.IsValid)
                {
                    entity = await _context.Categories.FindAsync(id);
                    if (entity == null)
                        return NotFound();

                    return View(category);
                }
            }

            if (category.FormFile != null)
            {
                if (!category.FormFile.FileTypeAsync("image/"))
                {
                    ModelState.AddModelError("FormFile", "Wrong file type");
                    return View(category);
                }

                if (category.FormFile.FileSize(2))
                {
                    ModelState.AddModelError("FormFile", "File max size is 2mb");
                    return View(category);
                }

                if (!string.IsNullOrEmpty(entity.Icon))
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, "admin", "icons", "categories", entity.Icon);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string uploadFolder = Path.Combine(_environment.WebRootPath, "admin", "icons", "categories");
                string uniqueFileName = await category.FormFile.SaveToAsync(uploadFolder);
                entity.Icon = uniqueFileName;
            }

            entity.Name = category.Name.Trim();
            entity.IPAddress = IpAddress;
            entity.ModifiedBy = 1;
            entity.Modified = DateTime.UtcNow;
            entity.IsDeleted = false;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

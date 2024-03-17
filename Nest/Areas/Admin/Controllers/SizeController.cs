using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Areas.Admin.ViewModels;
using Nest.Data;
using Nest.Models;

namespace Nest.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        public readonly NestContext _context;

        public SizeController(NestContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sizes = await _context.Sizes.Where(s => !s.IsDeleted).ToListAsync();
            SizeVM sizeVM = new SizeVM
            {
                Sizes = sizes
            };
            return View(sizeVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(SizeVM sizeVM)
        {
            var newSize = new Size
            {
                Name = sizeVM.Size.Name,
                IsDeleted = false,
                CreateBy = 1,
                Created = DateTime.UtcNow,
                IPAddress = "1"
            };

            await _context.Sizes.AddAsync(newSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Size size = await _context.Sizes.Where(s => !s.IsDeleted && s.Id == id).FirstOrDefaultAsync();
            size.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeletedList()
        {
            return View(await _context.Sizes.Where(s => s.IsDeleted).ToListAsync());
        }
        public async Task<IActionResult> ListHardDeleted(int id)
        {
            Size size = await _context.Sizes.Where(s => s.IsDeleted && s.Id == id).FirstOrDefaultAsync();
            _context.Sizes.Remove(size);
            _context.SaveChanges();
            return RedirectToAction(nameof(DeletedList));
        }

        public async Task<IActionResult> ListReturnDeleted(int id)
        {
            var size = await _context.Sizes.Where(s => s.IsDeleted && s.Id == id).FirstOrDefaultAsync();
            size.IsDeleted = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DeletedList));
        }

    }
}

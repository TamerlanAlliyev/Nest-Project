using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Areas.Admin.ViewModels;
using Nest.Data;
using Nest.Models;

namespace Nest.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class WeightController : Controller
    {
        public readonly NestContext _context;

        public WeightController(NestContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var weight = await _context.Weights.Where(w => !w.IsDeleted).ToListAsync();
            WeightVM weightVM = new WeightVM
            {
                Weights = weight,
            };
            return View(weightVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult >Create(WeightVM weightVM)
        {
            var Weight = new Weight
            {
                Gram = weightVM.Weight.Gram,
                IsDeleted = false,
                CreateBy = 1,
                Created = DateTime.UtcNow,
                IPAddress = "1"
            };

            await _context.Weights.AddAsync(Weight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //public Task<IActionResult> Create(int id)
        //{

        //}
        //public Task<IActionResult> Create(int id)
        //{

        //}
    }
}

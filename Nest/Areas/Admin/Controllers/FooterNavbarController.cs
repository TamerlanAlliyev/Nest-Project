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

    public class FooterNavbarController : Controller
    {
        public readonly NestContext _context;

        public FooterNavbarController(NestContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Navbar = await _context.FooterHeads.Where(fh => !fh.IsDeleted)
                                                   .Include(fh => fh.FooterSubs)
                                                   .OrderBy(fh => fh.Order)
                                                   .ToListAsync();
            return View(Navbar);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var Head = await _context.FooterHeads.Where(fh => !fh.IsDeleted && fh.Id == id)
                                                  .Include(fh => fh.FooterSubs.OrderBy(fs => fs.Order))
                                                  .FirstOrDefaultAsync();


            FooterVM footer = new FooterVM()
            {
                FooterHead = Head
            };
            await _context.SaveChangesAsync();
            return View(footer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Head = await _context.FooterHeads.Where(fh => !fh.IsDeleted && fh.Id == id)
                                                   .FirstOrDefaultAsync();

             _context.FooterHeads.Remove(Head);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FooterHeads heads)
        {


            var navHead = new FooterHeads()
            {
                Head = heads.Head,
                Order = heads.Order,
                IsDeleted = false,
                CreateBy = 1,
                Created = DateTime.UtcNow,
                IPAddress = "1"
            };

            await _context.FooterHeads.AddAsync(navHead);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var Head = await _context.FooterHeads.Where(fh => !fh.IsDeleted && fh.Id == id)
                                                                 .Include(fh => fh.FooterSubs)
                                                                 .FirstOrDefaultAsync();
            return View(Head);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,FooterHeads heads)
        {

            var upHeads = await _context.FooterHeads.Where(fh => !fh.IsDeleted && fh.Id == id)
                                                                 .Include(fh => fh.FooterSubs)
                                                                 .FirstOrDefaultAsync();

            upHeads.Head = heads.Head;
            upHeads.Order = heads.Order;
            upHeads.Modified = DateTime.UtcNow;
            upHeads.ModifiedBy = 1;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }





        #region SUB  LIST CRAT

        public IActionResult SubListCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubListCreate(int id, FooterSub sub)
        {
            FooterSub SubNavbar = new FooterSub
            {
                SubList = sub.SubList,
                Order = sub.Order,
                FooterHeadsId = id,
                IsDeleted = false,
                CreateBy = 1,
                Created = DateTime.UtcNow,
                IPAddress = "1",
            };
            await _context.FooterSubs.AddAsync(SubNavbar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Detail), new { id = id });
        }

        public async Task<IActionResult> SubListDetail(int id)
        {
            var subList = await _context.FooterSubs
                              .Where(sl => !sl.IsDeleted && sl.Id == id)
                              .Include(sl => sl.FooterHeads)
                              .FirstOrDefaultAsync();

            return View(subList);
        }

        public async Task<IActionResult> SubListDelete(int id, int head)
        {
            var subList = await _context.FooterSubs
                              .Where(sl => !sl.IsDeleted && sl.Id == id)
                              .Include(sl => sl.FooterHeads)
                              .FirstOrDefaultAsync();
            _context.FooterSubs.Remove(subList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Detail), new { id = head });
        }

        public async Task<IActionResult> SubListUpdate(int id)
        {
            var subList = await _context.FooterSubs
                .Where(sl => !sl.IsDeleted && sl.Id == id)
                .Include(sl => sl.FooterHeads)
                .FirstOrDefaultAsync();

            if (subList == null)
            {
                return NotFound();
            }

            return View(subList);
        }

        [HttpPost]
        public async Task<IActionResult> SubListUpdate(int id, FooterSub sub)
        {
            if (id != sub.Id)
            {
                return BadRequest();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View(sub);
            //}

            var subList = await _context.FooterSubs
                .Where(sl => !sl.IsDeleted && sl.Id == id)
                .Include(sl => sl.FooterHeads)
                .FirstOrDefaultAsync();

            if (subList == null)
            {
                return NotFound();
            }

            subList.Order = sub.Order;
            subList.SubList = sub.SubList;
            subList.IsDeleted = false;
            subList.ModifiedBy = 1;
            subList.Modified = DateTime.UtcNow;
            subList.IPAddress = "1";

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Detail), new { id = subList.FooterHeadsId });
        }
        #endregion
    }
}

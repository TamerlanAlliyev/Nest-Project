using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Data;

namespace Nest.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
       public readonly NestContext _context;

        public FooterViewComponent(NestContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Navbar = await _context.FooterHeads.Where(f=>!f.IsDeleted)
                                                   .Include(f=>f.FooterSubs.OrderBy(fs=>fs.Order))
                                                   .OrderBy(f=>f.Order)
                                                   .ToListAsync();
            return View(Navbar);
        }
    }
}

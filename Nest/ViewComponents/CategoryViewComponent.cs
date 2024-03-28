using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Data;

namespace Nest.ViewComponents
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly NestContext _context;

        public CategoryViewComponent(NestContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult >InvokeAsync()
        {
            var categories = await _context.Categories.Where(c=>!c.IsDeleted).ToListAsync();
            return View(categories);
        }
    }
}

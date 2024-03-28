using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest.Data;
using Nest.ViewModels;
using Newtonsoft.Json;

namespace Nest.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly NestContext _context;

        public CartViewComponent(NestContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketVM>? BasketVM = GetBasket();
            List<BasketItemVM> BasketItemVM = new List<BasketItemVM>();
            foreach (var basketData in BasketVM)
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == basketData.Id);
                BasketItemVM.Add(new BasketItemVM()
                {
                    Count = basketData.Count,
                    Id = basketData.Id,
                    Image = product.ProductImages.FirstOrDefault(x => x.IsMain).Url,
                    Price = product.SellPrice,
                    Name = product.Name,
                });
            }

            return View(BasketItemVM);
        }
        private List<BasketVM> GetBasket()
        {
            List<BasketVM> BasketVMs;
            if (Request.Cookies["basket"] != null)
            {
                BasketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else BasketVMs = new List<BasketVM>();
            return BasketVMs;
        }

    }
}

using Nest.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nest.Areas.Admin.ViewModels
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public List<int >SizeId { get; set; }
        public List<int> SizeCount { get; set; } = default;
        //public List<Size> SizeList { get; set; }=new {List<Size>()};
        [NotMapped]
        public List<IFormFile>? Files { get; set; }
        [NotMapped]
        public IFormFile? MainFile { get; set; }
        [NotMapped]
        public IFormFile? HoverFile { get; set; }
    }
}

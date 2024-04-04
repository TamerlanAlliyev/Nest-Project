using Nest.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nest.Areas.Admin.ViewModels
{
    public class ProductUpdateVM
    {
        List<ProductSize> Sizes = new List<ProductSize>();
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public List<int> SizeId { get; set; }
        public List<int> SizeCount { get; set; }
        //public List<Size> SizeList { get; set; }=new {List<Size>()};
        [NotMapped]
        public List<IFormFile>? Files { get; set; }
        [NotMapped]
        public IFormFile MainFile { get; set; }
        [NotMapped]
        public IFormFile HoverFile { get; set; }

        public List<ProductImage>? InFiles { get; set; }
        public ProductImage InMainFile { get; set; }
        public ProductImage InHoverFile { get; set; }




    }
}

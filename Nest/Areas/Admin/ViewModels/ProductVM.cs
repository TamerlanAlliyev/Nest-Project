using Nest.Models;

namespace Nest.Areas.Admin.ViewModels
{
    public class ProductVM
    {
        public List<Product> Products { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; }=null!;
        public ProductImage ProductIsMain { get; set; }=null!;
        public List<CustomerRating>? CustomerRatings { get; set; }
        public List<Size>? Size { get; set; }
        public List<Weight>? Weights { get; set; }

        //public List<Vendor> Vendors { get; set; }
    }
}

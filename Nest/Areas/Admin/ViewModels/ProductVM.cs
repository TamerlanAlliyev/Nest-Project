using Nest.Models;

namespace Nest.Areas.Admin.ViewModels
{
    public class ProductVM
    {
        public Product? Product { get; set; } = null!;
        public List<Product> Products { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; }=null!;
        public ProductImage ProductIsMain { get; set; }=null!;
        public List<CustomerRating>? CustomerRatings { get; set; }
        public List<Weight>? Weights { get; set; }
        public List<Category> Categories { get; set; }
        public List<Vendor> Vendors { get; set; }

        public List<Size>? Sizes { get; set; } 
        public ProductSize ProductSize { get; set; }
        public List<ProductSize >ProductSizes { get; set; }


        //public List<Vendor> Vendors { get; set; }
    }
}

using Nest.Models;

namespace Nest.Areas.Admin.ViewModels
{
    public class ProductVM
    {
        public List<Product> Products { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<CustomerRating> CustomerRatings { get; set; }
        public List<SizeWeight> SizeWeights { get; set; }

        //public List<Vendor> Vendors { get; set; }☻
    }
}

using Nest.Data;
using Nest.Models;

namespace Nest.ViewModels
{
    public class ProductVM
    {
        public Product? Product { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<Size> Size { get; set; }
        public List<Weight> Weights { get; set; }
        public List<CustomerRating> CustomerRatings { get; set; }

    }
}

using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class Product:BaseAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SellPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int CategoryId { get; set; }
        public int VendorsId { get; set; }
        public Vendor Vendor { get; set; }
        public List<Category> Category { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<CustomerRating> CustomerRatings { get; set; }
        public List<SizeWeight> SizeWeights { get; set; }

    }
}

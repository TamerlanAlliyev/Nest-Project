using Nest.Models.BaseEntitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nest.Models
{
    public class Product:BaseAuditable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal SellPrice { get; set; }
        public decimal? DiscountPrice { get; set; }

        //public int CategoryId { get; set; }
        //public List<Category> Category { get; set; } = null!;

        public ICollection<CategoryProduct>? CategoryProducts { get; set; }

        public int? VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

        public List<ProductImage>? ProductImages { get; set; } 
        public List<CustomerRating>? CustomerRatings { get; set; } 

        public List<ProductSize>? ProductSizes { get; set; } 
        public List<ProductWeight>? ProductWeights { get; set; }


        [NotMapped]
        public List<IFormFile >?Files { get; set; } 
        [NotMapped]
        public IFormFile? IsMainFile { get; set; } 
        [NotMapped]
        public IFormFile? IsHoverFile { get; set; } 


    }
}
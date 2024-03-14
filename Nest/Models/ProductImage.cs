using Nest.Models.BaseEntitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nest.Models
{
    public class ProductImage:BaseAuditable
    {
        public string Url { get; set; }
        public bool? IsMain { get; set; }
        public bool? IsHover { get; set; }
        public int ProductId { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; } = null!;
        public Product Product { get; set; }=null!;
    }
}

using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class ProductImage:BaseAuditable
    {
        public string Url { get; set; }
        public bool? IsMain { get; set; }
        public int ProductId { get; set; }
        public IFormFile FormFile { get; set; }
        public Product Product { get; set; }
    }
}

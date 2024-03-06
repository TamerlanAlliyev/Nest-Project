using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class Category:BaseAuditable
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public int ProductId { get; set; }
        public IFormFile FormFile { get; set; } = null!;

        public List<Product> Product { get; set; } = null!;
    }
}

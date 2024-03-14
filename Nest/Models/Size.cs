using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class Size:BaseAuditable
    {
        public string Name { get; set; }
        public List<ProductSize> ProductSizes { get; set; } 
    }
}

using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class Size:BaseAuditable
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

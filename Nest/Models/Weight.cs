using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class Weight : BaseAuditable
    {
        public int Gram { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}

using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class SizeWeight:BaseAuditable
    {
        public int Weight { get; set; }
        public int WeightCount { get; set; }
        public int Size { get; set; }
        public int SizeCount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}

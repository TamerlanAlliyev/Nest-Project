using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class Weight : BaseAuditable
    {
        public int Gram { get; set; }
        public List<ProductWeight>? ProductWeight { get; set; } 

    }
}

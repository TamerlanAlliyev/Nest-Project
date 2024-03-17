using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class FooterSub:BaseAuditable
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string SubList { get; set; }
        public int FooterHeadsId { get; set; }
        public FooterHeads FooterHeads { get; set; }
    }
}

using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class FooterHeads:BaseAuditable
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Head { get; set; }
        public List<FooterSub> FooterSubs { get; set; }
    }
}

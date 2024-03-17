using Nest.Models;

namespace Nest.Areas.Admin.ViewModels
{
    public class FooterVM
    {
        public List<FooterHeads> FooterHeads { get; set; }
        public FooterHeads FooterHead { get; set; }
        public List<FooterSub> FooterSubs { get; set; }
        public FooterSub FooterSub { get; set; } = new FooterSub();

    }
}

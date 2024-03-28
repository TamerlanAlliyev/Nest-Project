using Nest.Models;

namespace Nest.Areas.Admin.ViewModels
{
    public class WeightVM
    {
        public Weight Weight { get; set; }
        public List<Weight> Weights { get; set; }
    }
}

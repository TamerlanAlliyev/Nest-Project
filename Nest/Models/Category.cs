using Nest.Models.BaseEntitys;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nest.Models
{
    public class Category:BaseAuditable
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        //public int ProductId { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; } = null!;
        public List<Product> Product { get; set; } = null!;
    }
}

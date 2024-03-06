using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class CustomerRating:BaseAuditable
    {
        public int Evaluation { get; set; }
        public string? Comment { get; set; }
        public int CustomerId {  get; set; }
        public Customer Customer { get; set; } = null!;
        public int ProductId { get; set;}
        public Product Product { get; set; } = null!;
    }
}

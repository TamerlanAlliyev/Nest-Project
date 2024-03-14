namespace Nest.Models
{
    public class ProductWeight
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int WeightId { get; set; }
        public Weight Weight { get; set; }=null!;
        public int Count { get; set; }

    }
}

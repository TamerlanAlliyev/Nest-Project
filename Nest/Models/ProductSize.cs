namespace Nest.Models
{
    public class ProductSize
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int SizeId { get; set; }
        public Size Size { get; set; } = null!;
        public int Count { get; set; } 
    }
}

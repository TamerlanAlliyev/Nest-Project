using Nest.Models; // Assuming your models are in a namespace called "Nest.Models"

namespace Nest.Areas.Admin.ViewModels
{
    public class ProductDetailsVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        // Use decimal for currency representation
        public decimal ProductSellPrice { get; set; }
        public decimal? ProductDiscountPrice { get; set; } // Allow null for discounts

        public string ProductVendor { get; set; }

        public int ProductCreateBy { get; set; }

        // Use DateTimeOffset for time zone awareness (optional)
        public DateTimeOffset ProductCreated { get; set; }

        public int? ProductModifiedBy { get; set; }
        public DateTimeOffset? ProductModified { get; set; }

        public string ProductIPAddress { get; set; }

        public List<string> CategoryNames { get; set; } = new List<string>(); // Initialize to avoid null reference exceptions

        public List<string> ProductImages { get; set; } = new List<string>(); // Initialize to avoid null reference exceptions

        public string ProductIsMain { get; set; }

        public List<CustomerRating> CustomerRatings { get; set; } = new List<CustomerRating>(); // Initialize to avoid null reference exceptions

        public List<Size> ProductSizes { get; set; } = new List<Size>(); // Initialize to avoid null reference exceptions

        public List<Weight> ProductWeights { get; set; } = new List<Weight>(); // Initialize to avoid null reference exceptions
    }
}

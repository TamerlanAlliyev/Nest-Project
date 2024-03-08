﻿using Nest.Models.BaseEntitys;

namespace Nest.Models
{
    public class Product:BaseAuditable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal SellPrice { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;
        public List<Category> Category { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; } = null!;
        public List<CustomerRating>? CustomerRatings { get; set; } = null!;
        public List<SizeWeight> SizeWeights { get; set; } = null!;

    }
}

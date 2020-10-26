using System;

namespace PublicApi.DTO.v1
{
    public class ProductView
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        
        public string ProductType { get; set; } = default!;
        public Guid ProductTypeId { get; set; } = default!;

        public double Cost { get; set; }

        public string Symbol { get; set; } = default!;
        
        public string? Image { get; set; }
    }
}

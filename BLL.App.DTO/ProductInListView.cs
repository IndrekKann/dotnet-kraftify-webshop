using System;

namespace BLL.App.DTO
{
    public class ProductInListView
    {
        public Guid Id { get; set; }
        
        public string Product { get; set; } = default!;

        public Guid ProductId { get; set; }
        
        public double Cost { get; set; }
        
        public string Symbol { get; set; } = default!;
        
        public int Quantity { get; set; }
        
        public double TotalCost { get; set; }

        public string? Image { get; set; }
    }
}
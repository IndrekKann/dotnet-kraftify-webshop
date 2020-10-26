using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class ProductInList : DomainEntityIdMetadata
    {
        [Display(Name = nameof(ShoppingCart), ResourceType = typeof(Resources.Domain.ProductInList))]
        public Guid? ShoppingCartId { get; set; }
        [Display(Name = nameof(ShoppingCart), ResourceType = typeof(Resources.Domain.ProductInList))]
        public ShoppingCart? ShoppingCart { get; set; }

        [Display(Name = nameof(Order), ResourceType = typeof(Resources.Domain.ProductInList))]
        public Guid? OrderId { get; set; }
        [Display(Name = nameof(Order), ResourceType = typeof(Resources.Domain.ProductInList))]
        public Order? Order { get; set; }

        [Display(Name = nameof(Product), ResourceType = typeof(Resources.Domain.ProductInList))]
        public Guid ProductId { get; set; }
        [Display(Name = nameof(Product), ResourceType = typeof(Resources.Domain.ProductInList))]
        public Product? Product { get; set; }

        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.Domain.ProductInList))]
        public int Quantity { get; set; }
        
        [Display(Name = nameof(TotalCost), ResourceType = typeof(Resources.Domain.ProductInList))]
        public double TotalCost { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ProductInList : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(ShoppingCart), ResourceType = typeof(Resources.BLL.App.DTO.ProductInList))]
        public Guid? ShoppingCartId { get; set; }
        [Display(Name = nameof(ShoppingCart), ResourceType = typeof(Resources.BLL.App.DTO.ProductInList))]
        public ShoppingCart? ShoppingCart { get; set; }
        
        [Display(Name = nameof(Order), ResourceType = typeof(Resources.BLL.App.DTO.ProductInList))]
        public Guid? OrderId { get; set; }
        [Display(Name = nameof(Order), ResourceType = typeof(Resources.BLL.App.DTO.ProductInList))]
        public Order? Order { get; set; }

        [Display(Name = nameof(Product), ResourceType = typeof(Resources.BLL.App.DTO.ProductInList))]
        public Guid ProductId { get; set; }
        [Display(Name = nameof(Product), ResourceType = typeof(Resources.BLL.App.DTO.ProductInList))]
        public Product? Product { get; set; }

        [Display(Name = nameof(Quantity), ResourceType = typeof(Resources.BLL.App.DTO.ProductInList))]
        public int Quantity { get; set; }

        [Display(Name = nameof(TotalCost), ResourceType = typeof(Resources.BLL.App.DTO.ProductInList))]
        public double TotalCost { get; set; }
    }
}

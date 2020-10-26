using System;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace DAL.App.DTO
{
    public class ProductInList : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid? ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }

        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        
        public double TotalCost { get; set; }
    }
}

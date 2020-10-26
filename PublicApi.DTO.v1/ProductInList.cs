using System;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class ProductInList : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ShoppingCartId { get; set; }

        public Guid ProductId { get; set; }

        public double TotalCost { get; set; }

        public int Quantity { get; set; }
        
    }
}

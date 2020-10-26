using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }

        [MaxLength(128)]
        public string OrderNumber { get; set; } = default!;

        public DateTime Date { get; set; }

        public double TotalCost { get; set; }

        public ICollection<Payment>? Payments { get; set; }
        
        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

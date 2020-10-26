using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid AppUserId { get; set; }

        public Guid ShoppingCartId { get; set; }

        [MaxLength(128)]
        [Required]
        public string OrderNumber { get; set; } = default!;

        public DateTime Date { get; set; }

        public double TotalCost { get; set; }
    }
}

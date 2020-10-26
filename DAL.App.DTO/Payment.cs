using System;
using ee.itcollege.webshop.indrek.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        
        public Guid DestinationId { get; set; }
        public Destination? Destination { get; set; }

        public DateTime Date { get; set; }
    }
}

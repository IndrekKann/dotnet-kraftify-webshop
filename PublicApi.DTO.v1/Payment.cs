using System;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        
        public Guid PaymentTypeId { get; set; }

        public Guid OrderId { get; set; }
        
        public Guid DestinationId { get; set; }

        public DateTime Date { get; set; }
    }
}
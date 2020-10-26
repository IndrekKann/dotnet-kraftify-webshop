using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class PaymentType : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid NameId { get; set; }
        [MaxLength(128)]
        public string Name { get; set; } = default!;
    }
}

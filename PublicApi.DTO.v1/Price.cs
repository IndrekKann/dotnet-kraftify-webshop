using System;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Price: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid CurrencyId { get; set; }

        public double Cost { get; set; }
    }
}

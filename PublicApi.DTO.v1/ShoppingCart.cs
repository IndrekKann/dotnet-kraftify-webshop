using System;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class ShoppingCart : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
    }
}

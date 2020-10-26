using System;
using System.Collections.Generic;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Price: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        public double Cost { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}

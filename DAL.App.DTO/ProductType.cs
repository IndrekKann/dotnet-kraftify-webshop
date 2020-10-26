using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace DAL.App.DTO
{
    public class ProductType : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid NameId { get; set; }
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        public ICollection<Product>? Products { get; set; }
    }
}

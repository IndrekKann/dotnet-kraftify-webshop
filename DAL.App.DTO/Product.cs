using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;
using Microsoft.AspNetCore.Http;

namespace DAL.App.DTO
{
    public class Product : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }

        public Guid PriceId { get; set; }
        public Price? Price { get; set; }

        public Guid NameId { get; set; }
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        public Guid DescriptionId { get; set; }
        [MaxLength(4096)]
        public string Description { get; set; } = default!;
        
        public string? Image { get; set; }

        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Product : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid ProductTypeId { get; set; }

        public Guid PriceId { get; set; }

        public Guid NameId { get; set; }
        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string Name { get; set; } = default!;
        
        public Guid DescriptionId { get; set; }
        [MinLength(1)]
        [MaxLength(4096)]
        [Required]        
        public string Description { get; set; } = default!;
        
        [MaxLength(4096)]
        public string? Image { get; set; }
    }
}

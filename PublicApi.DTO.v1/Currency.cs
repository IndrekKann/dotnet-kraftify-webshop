using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1
{
    public class Currency : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(128)]
        [Required]
        public string Name { get; set; } = default!;

        [MaxLength(3)]
        [Required]
        public string Abbreviation { get; set; } = default!;
        
        [MaxLength(1)]
        public string Symbol { get; set; } = default!;
    }
}

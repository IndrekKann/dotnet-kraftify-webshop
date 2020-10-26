using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Currency : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        [MaxLength(3)]
        public string Abbreviation { get; set; } = default!;
        
        [MaxLength(1)]
        public string Symbol { get; set; } = default!;

        public ICollection<Price>? Prices { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace DAL.App.DTO
{
    public class Destination : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(128)] 
        public string Location { get; set; } = default!;

        public ICollection<Payment>? Payments { get; set; }
    }
}
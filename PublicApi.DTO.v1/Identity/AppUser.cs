using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace PublicApi.DTO.v1.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string FirstName { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string LastName { get; set; } = default!;

        [MinLength(7)]
        [MaxLength(8)]
        [Required]
        public string Phone { get; set; } = default!;

    }
}
using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public virtual string Email { get; set; } = default!;
        public virtual string UserName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public virtual string FirstName { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public virtual string LastName { get; set; } = default!;
        
        [MinLength(7)]
        [MaxLength(8)]
        [Required]
        public virtual string Phone { get; set; } = default!;
    }
}
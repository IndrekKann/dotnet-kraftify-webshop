using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId 
    {
        [MaxLength(64)]
        [MinLength(1)]
        public virtual string FirstName { get; set; } = default!;

        [MaxLength(64)]
        [MinLength(1)]
        public virtual string LastName { get; set; } = default!;

        [MaxLength(8)]
        [MinLength(7)]
        [Phone]
        public virtual string Phone { get; set; } = default!;
    }
}
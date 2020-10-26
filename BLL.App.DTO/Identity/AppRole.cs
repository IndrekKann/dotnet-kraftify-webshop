using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO.Identity
{
    public class AppRole : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public virtual string DisplayName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public virtual string Name { get; set; } = default!;

    }
}
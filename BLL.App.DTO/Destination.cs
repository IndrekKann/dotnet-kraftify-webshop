using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Destination : IDomainEntityId
    {
        public Guid Id { get; set; }

        [Display(Name = nameof(Location), ResourceType = typeof(Resources.BLL.App.DTO.Destination))]
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Location { get; set; } = default!;

        [Display(Name = nameof(Payments), ResourceType = typeof(Resources.BLL.App.DTO.Destination))]
        public ICollection<Payment>? Payments { get; set; }
    }
}
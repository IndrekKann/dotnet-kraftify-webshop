using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class Destination : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Location), ResourceType = typeof(Resources.Domain.Destination))]
        public string Location { get; set; } = default!;

        [Display(Name = nameof(Payments), ResourceType = typeof(Resources.Domain.Destination))]
        public ICollection<Payment>? Payments { get; set; }
    }
}
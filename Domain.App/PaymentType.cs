using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class PaymentType : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.PaymentType))]
        public Guid NameId { get; set; }
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.PaymentType))]
        public LangStr? Name { get; set; }

        [Display(Name = nameof(Payments), ResourceType = typeof(Resources.Domain.PaymentType))]
        public ICollection<Payment>? Payments { get; set; }
    }
}

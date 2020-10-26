using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class Payment : DomainEntityIdMetadataUser<AppUser>
    {
        [Display(Name = nameof(PaymentType), ResourceType = typeof(Resources.Domain.Payment))]
        public Guid PaymentTypeId { get; set; }
        [Display(Name = nameof(PaymentType), ResourceType = typeof(Resources.Domain.Payment))]
        public PaymentType? PaymentType { get; set; }

        [Display(Name = nameof(Order), ResourceType = typeof(Resources.Domain.Payment))]
        public Guid OrderId { get; set; }
        [Display(Name = nameof(Order), ResourceType = typeof(Resources.Domain.Payment))]
        public Order? Order { get; set; }
        
        [Display(Name = nameof(Destination), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public Guid DestinationId { get; set; }
        [Display(Name = nameof(Destination), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public Destination? Destination { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = nameof(Date), ResourceType = typeof(Resources.Domain.Payment))]
        public DateTime Date { get; set; }
    }
}

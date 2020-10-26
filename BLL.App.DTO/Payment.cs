using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Payment : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(PaymentType), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public Guid PaymentTypeId { get; set; }
        [Display(Name = nameof(PaymentType), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public PaymentType? PaymentType { get; set; }

        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public Guid AppUserId { get; set; }
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public AppUser? AppUser { get; set; }

        [Display(Name = nameof(Order), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public Guid OrderId { get; set; }
        [Display(Name = nameof(Order), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public Order? Order { get; set; }

        [Display(Name = nameof(Destination), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public Guid DestinationId { get; set; }
        [Display(Name = nameof(Destination), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public Destination? Destination { get; set; }

        [Display(Name = nameof(Date), ResourceType = typeof(Resources.BLL.App.DTO.Payment))]
        public DateTime Date { get; set; }
    }
}

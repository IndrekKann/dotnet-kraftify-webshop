using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public Guid AppUserId { get; set; }
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public AppUser? AppUser { get; set; }

        [Display(Name = nameof(ShoppingCart), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public Guid ShoppingCartId { get; set; }
        [Display(Name = nameof(ShoppingCart), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public ShoppingCart? ShoppingCart { get; set; }

        [Display(Name = nameof(OrderNumber), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        [MaxLength(4096, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string OrderNumber { get; set; } = default!;

        [Display(Name = nameof(Date), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public DateTime Date { get; set; }

        [Display(Name = nameof(TotalCost), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public double TotalCost { get; set; }

        [Display(Name = nameof(Payments), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public ICollection<Payment>? Payments { get; set; }
        
        [Display(Name = nameof(ProductInLists), ResourceType = typeof(Resources.BLL.App.DTO.Order))]
        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

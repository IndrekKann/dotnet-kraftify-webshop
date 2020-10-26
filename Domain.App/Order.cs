using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class Order : DomainEntityIdMetadataUser<AppUser>
    {
        [Display(Name = nameof(ShoppingCart), ResourceType = typeof(Resources.Domain.Order))]
        public Guid ShoppingCartId { get; set; }
        [Display(Name = nameof(ShoppingCart), ResourceType = typeof(Resources.Domain.Order))]
        public ShoppingCart? ShoppingCart { get; set; }

        [MaxLength(64)]
        [Display(Name = nameof(OrderNumber), ResourceType = typeof(Resources.Domain.Order))]
        public string OrderNumber { get; set; }  = default!;

        [DataType(DataType.Date)]
        [Display(Name = nameof(Date), ResourceType = typeof(Resources.Domain.Order))]
        public DateTime Date { get; set; }

        [Display(Name = nameof(TotalCost), ResourceType = typeof(Resources.Domain.Order))]
        public double TotalCost { get; set; }

        [Display(Name = nameof(Payments), ResourceType = typeof(Resources.Domain.Order))]
        public ICollection<Payment>? Payments { get; set; }
        
        [Display(Name = nameof(ProductInLists), ResourceType = typeof(Resources.Domain.Order))]
        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

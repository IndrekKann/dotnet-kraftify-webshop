using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ShoppingCart : IDomainEntityId
    {
        public Guid Id { get; set; }

        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.ShoppingCart))]
        public Guid AppUserId { get; set; }
        [Display(Name = nameof(AppUser), ResourceType = typeof(Resources.BLL.App.DTO.ShoppingCart))]
        public AppUser? AppUser { get; set; }

        [Display(Name = nameof(ProductInLists), ResourceType = typeof(Resources.BLL.App.DTO.ShoppingCart))]
        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

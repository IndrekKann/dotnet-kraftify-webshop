using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class ShoppingCart : DomainEntityIdMetadataUser<AppUser>
    {
        [Display(Name = nameof(ProductInLists), ResourceType = typeof(Resources.Domain.ShoppingCart))]
        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

using System;
using System.Collections.Generic;
using ee.itcollege.webshop.indrek.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ShoppingCart : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

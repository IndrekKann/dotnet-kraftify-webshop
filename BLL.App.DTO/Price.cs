using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Price: IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(Currency), ResourceType = typeof(Resources.BLL.App.DTO.Price))]
        public Guid CurrencyId { get; set; }
        [Display(Name = nameof(Currency), ResourceType = typeof(Resources.BLL.App.DTO.Price))]
        public Currency? Currency { get; set; }

        [Display(Name = nameof(Cost), ResourceType = typeof(Resources.BLL.App.DTO.Price))]
        public double Cost { get; set; }

        [Display(Name = nameof(Products), ResourceType = typeof(Resources.BLL.App.DTO.Price))]
        public ICollection<Product>? Products { get; set; }
    }
}

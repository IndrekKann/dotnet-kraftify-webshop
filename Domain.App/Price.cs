using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class Price : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Currency), ResourceType = typeof(Resources.Domain.Price))]
        public Guid CurrencyId { get; set; }
        [Display(Name = nameof(Currency), ResourceType = typeof(Resources.Domain.Price))]
        public Currency? Currency { get; set; }

        [Display(Name = nameof(Cost), ResourceType = typeof(Resources.Domain.Price))]
        public double Cost { get; set; }
        
        [Display(Name = nameof(Products), ResourceType = typeof(Resources.Domain.Price))]
        public ICollection<Product>? Products { get; set; }
    }
}

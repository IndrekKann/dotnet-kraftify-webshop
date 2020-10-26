using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class ProductType : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.ProductType))]
        public Guid NameId { get; set; }
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.ProductType))]
        public LangStr? Name { get; set; }

        [Display(Name = nameof(Products), ResourceType = typeof(Resources.Domain.ProductType))]
        public ICollection<Product>? Products { get; set; }
    }
}

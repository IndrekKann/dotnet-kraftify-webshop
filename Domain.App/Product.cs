using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class Product : DomainEntityIdMetadata
    {
        [Display(Name = nameof(ProductType), ResourceType = typeof(Resources.Domain.Product))]
        public Guid ProductTypeId { get; set; }
        [Display(Name = nameof(ProductType), ResourceType = typeof(Resources.Domain.Product))]
        public ProductType? ProductType { get; set; }

        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Product))]
        public Guid PriceId { get; set; }
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.Domain.Product))]
        public Price? Price { get; set; }

        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Product))]
        public Guid NameId { get; set; }
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Product))]
        public LangStr? Name { get; set; }
        
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Product))]
        public Guid DescriptionId { get; set; }
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.Domain.Product))]
        public LangStr? Description { get; set; }

        [Display(Name = nameof(Image), ResourceType = typeof(Resources.Domain.Product))]
        public string? Image { get; set; }

        [Display(Name = nameof(ProductInLists), ResourceType = typeof(Resources.Domain.Product))]
        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

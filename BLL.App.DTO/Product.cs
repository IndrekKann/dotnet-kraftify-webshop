using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;
using Microsoft.AspNetCore.Http;

namespace BLL.App.DTO
{
    public class Product : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = nameof(ProductType), ResourceType = typeof(Resources.BLL.App.DTO.Product))]
        public Guid ProductTypeId { get; set; }
        [Display(Name = nameof(ProductType), ResourceType = typeof(Resources.BLL.App.DTO.Product))]
        public ProductType? ProductType { get; set; }

        [Display(Name = nameof(Price), ResourceType = typeof(Resources.BLL.App.DTO.Product))]
        public Guid PriceId { get; set; }
        [Display(Name = nameof(Price), ResourceType = typeof(Resources.BLL.App.DTO.Product))]
        public Price? Price { get; set; }

        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.Product))]
        [MaxLength(64, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Name { get; set; } = default!;
        
        [Display(Name = nameof(Description), ResourceType = typeof(Resources.BLL.App.DTO.Product))]
        [MaxLength(4096, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Description { get; set; } = default!;
        
        [Display(Name = nameof(Image), ResourceType = typeof(Resources.BLL.App.DTO.Product))]
        public string? Image { get; set; }

        [Display(Name = nameof(ProductInLists), ResourceType = typeof(Resources.BLL.App.DTO.Product))]
        public ICollection<ProductInList>? ProductInLists { get; set; }
    }
}

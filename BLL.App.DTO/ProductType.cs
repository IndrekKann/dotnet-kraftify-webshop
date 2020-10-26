using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ProductType : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public Guid NameId { get; set; }
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.ProductType))]
        [MaxLength(128, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Name { get; set; } = default!;

        [Display(Name = nameof(Products), ResourceType = typeof(Resources.BLL.App.DTO.ProductType))]
        public ICollection<Product>? Products { get; set; }
    }
}

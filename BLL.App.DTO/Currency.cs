using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace BLL.App.DTO
{
    public class Currency : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid NameId { get; set; }
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.BLL.App.DTO.Currency))]
        [MinLength(1, ErrorMessageResourceName = "ErrorMessage_MinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(256, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Name { get; set; } = default!;

        public Guid AbbreviationId { get; set; }
        [Display(Name = nameof(Abbreviation), ResourceType = typeof(Resources.BLL.App.DTO.Currency))]
        [MaxLength(3, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "ErrorMessage_Required", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Abbreviation { get; set; } = default!;

        public Guid? SymbolId { get; set; }
        [Display(Name = nameof(Symbol), ResourceType = typeof(Resources.BLL.App.DTO.Currency))]
        [MaxLength(1, ErrorMessageResourceName = "ErrorMessage_MaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Symbol { get; set; } = default!;
        
        [Display(Name = nameof(Prices), ResourceType = typeof(Resources.BLL.App.DTO.Currency))]
        public ICollection<Price>? Prices { get; set; }
    }
}

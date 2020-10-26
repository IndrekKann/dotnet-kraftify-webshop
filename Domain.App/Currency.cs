using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class Currency : DomainEntityIdMetadata
    {
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Currency))]
        public Guid NameId { get; set; }
        [Display(Name = nameof(Name), ResourceType = typeof(Resources.Domain.Currency))]
        public LangStr? Name { get; set; }
        
        [Display(Name = nameof(Abbreviation), ResourceType = typeof(Resources.Domain.Currency))]
        public Guid AbbreviationId { get; set; }
        [Display(Name = nameof(Abbreviation), ResourceType = typeof(Resources.Domain.Currency))]
        public LangStr? Abbreviation { get; set; }
        
        [Display(Name = nameof(Symbol), ResourceType = typeof(Resources.Domain.Currency))]
        public Guid SymbolId { get; set; }
        [Display(Name = nameof(Symbol), ResourceType = typeof(Resources.Domain.Currency))]
        public LangStr? Symbol { get; set; }
        
        [Display(Name = nameof(Prices), ResourceType = typeof(Resources.Domain.Currency))]
        public ICollection<Price>? Prices { get; set; }
    }
}

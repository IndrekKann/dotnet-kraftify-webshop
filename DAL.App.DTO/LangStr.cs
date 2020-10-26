using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.webshop.indrek.Contracts.Domain;

namespace DAL.App.DTO
{
    public class LangStr : IDomainEntityId
    {
        public Guid Id { get; set; }

        public ICollection<LangStrTranslation>? Translations { get; set; }
        
        [InverseProperty(nameof(Product.Name))]
        public ICollection<Product>? ProductNames { get; set; }
        
        [InverseProperty(nameof(Product.Description))]
        public ICollection<Product>? ProductDescriptions { get; set; }
        
        [InverseProperty(nameof(ProductType.Name))]
        public ICollection<ProductType>? ProductTypeNames { get; set; }
        
        [InverseProperty(nameof(PaymentType.Name))]
        public ICollection<PaymentType>? PaymentTypeNames { get; set; }
        
        [InverseProperty(nameof(Currency.Name))]
        public ICollection<Currency>? CurrencyNames { get; set; }
        
        [InverseProperty(nameof(Currency.Abbreviation))]
        public ICollection<Currency>? CurrencyAbbreviations { get; set; }
        
        [InverseProperty(nameof(Currency.Symbol))]
        public ICollection<Currency>? CurrencySymbols { get; set; }
    }
}
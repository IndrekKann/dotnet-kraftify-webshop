#pragma warning disable 1591
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class ProductIndexViewModel
    {
        public Product? Product { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<ProductType>? ProductTypes { get; set; }
        public string? Order { get; set; }
        public IEnumerable<SelectListItem>? OrderBySelectList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem {Text = Resources.Views.Products.NameAZ, Value = "1", Selected = true},
            new SelectListItem {Text = Resources.Views.Products.NameZA, Value = "2"},
            new SelectListItem {Text = Resources.Views.Products.PriceLowToHigh, Value = "3"},
            new SelectListItem {Text = Resources.Views.Products.PriceHighToLow, Value = "4"}
        };
        public string? Limit { get; set; }
        public IEnumerable<SelectListItem>? ProductsPerPageSelectList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem {Text = "18", Value = "18", Selected = true},
            new SelectListItem {Text = "36", Value = "36"},
            new SelectListItem {Text = "72", Value = "72"}
        };
        public IEnumerable<string>? Pages { get; set; }
        public string? CurrentURL { get; set; }
    }
}

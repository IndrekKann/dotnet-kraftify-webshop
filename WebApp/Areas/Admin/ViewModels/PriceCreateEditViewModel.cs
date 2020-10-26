#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class PriceCreateEditViewModel
    {
        public BLL.App.DTO.Price? Price { get; set; }
        
        public SelectList? CurrencySelectList { get; set; }
    }
}
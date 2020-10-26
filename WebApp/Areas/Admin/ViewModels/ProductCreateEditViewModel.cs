#pragma warning disable 1591
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class ProductCreateEditViewModel
    {
        public BLL.App.DTO.Product? Product { get; set; }
        
        public SelectList? ProductTypeSelectList { get; set; }

        public SelectList? PriceSelectList { get; set; }

        public IFormFile? Image { get; set; }
    }
}
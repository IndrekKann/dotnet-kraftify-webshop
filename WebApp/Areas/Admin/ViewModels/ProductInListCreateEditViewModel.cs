#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class ProductInListCreateEditViewModel
    {
        public BLL.App.DTO.ProductInList? ProductInList { get; set; }

        public SelectList? ShoppingCartSelectList { get; set; }

        public SelectList? ProductSelectList { get; set; }
    }
}
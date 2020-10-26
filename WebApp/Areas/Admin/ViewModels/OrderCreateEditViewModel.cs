#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class OrderCreateEditViewModel
    {
        public BLL.App.DTO.Order? Order { get; set; }

        public SelectList? ShoppingCartSelectList { get; set; }
    }
}
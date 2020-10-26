#pragma warning disable 1591
using System.Collections.Generic;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart? ShoppingCart { get; set; }
        public IEnumerable<ProductInList>? Products { get; set; }
        public double SubTotal { get; set; }
        public SelectList? PaymentTypeSelectList { get; set; }
        public SelectList? DestinationSelectList { get; set; }
        public Payment? Payment { get; set; }
        public Order? Order { get; set; }
    }
}
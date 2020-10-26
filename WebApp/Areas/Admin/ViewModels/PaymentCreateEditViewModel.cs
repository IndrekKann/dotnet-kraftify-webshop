#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class PaymentCreateEditViewModel
    {
        public BLL.App.DTO.Payment? Payment { get; set; }

        public SelectList? PaymentTypeSelectList { get; set; }

        public SelectList? OrderSelectList { get; set; }

        public SelectList? DestinationSelectList { get; set; }
    }
}
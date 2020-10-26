#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IAppBLL _bll;

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.Orders.FirstOrDefaultAsync(id.Value);

            var scId = _bll.ShoppingCarts.GetByAppUserId(User.UserGuidId()).Id;
            
            var vm = new ShoppingCartViewModel
            {
                ShoppingCart = await _bll.ShoppingCarts.FirstOrDefaultAsync(scId),
                Order = await _bll.Orders.FirstOrDefaultAsync(id.Value),
                Products = await _bll.ProductInLists.GetProductsForShoppingCartAsync(scId),
                SubTotal = await _bll.ProductInLists.GetTotalCost(scId),
                PaymentTypeSelectList = new SelectList(
                    _bll.PaymentTypes.GetAllAsync().Result, 
                    nameof(PaymentType.Id), nameof(PaymentType.Name)),
                DestinationSelectList = new SelectList(
                _bll.Destinations.GetAllAsync().Result,
                nameof(Destination.Id), nameof(Destination.Location))
            };

            if (order == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        public async Task<IActionResult> MakePayment(ShoppingCartViewModel vm, string orderId)
        {
            await _bll.Payments.MakePayment(vm.Payment!.PaymentTypeId, vm.Payment!.DestinationId, Guid.Parse(orderId), User.UserGuidId());
            await _bll.Orders.CopyShoppingCart(_bll.ShoppingCarts.GetByAppUserId(User.UserGuidId()).Id, Guid.Parse(orderId));
            await _bll.ShoppingCarts.ClearShoppingCart(_bll.ShoppingCarts.GetByAppUserId(User.UserGuidId()).Id);
            await _bll.SaveChangesAsync();

            return RedirectToAction("Index", "Products");
        }

    }
}

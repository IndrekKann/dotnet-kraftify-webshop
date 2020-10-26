#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class ShoppingCartsController : Controller
    {
        private readonly IAppBLL _bll;

        public ShoppingCartsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var sc = _bll.ShoppingCarts.GetByAppUserId(User.UserGuidId());

            var vm = new ShoppingCartViewModel
            {
                ShoppingCart = await _bll.ShoppingCarts.FirstOrDefaultAsync(sc.Id),
                Products = await _bll.ProductInLists.GetProductsForShoppingCartAsync(sc.Id),
                SubTotal = await _bll.ProductInLists.GetTotalCost(sc.Id)
            };

            return View(vm);
        }

        public async Task<IActionResult> PlaceOrder(Guid id)
        {
            if (id != null)
            {
                var orderId = await _bll.Orders.PlaceOrder(id, User.UserGuidId());
                await _bll.SaveChangesAsync();
                
                return RedirectToAction("Details", "Orders", new { id = orderId });
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromShoppingCart(Guid? id)
        {
            if (id != null)
            {
                await _bll.ProductInLists.RemoveFromShoppingCart(id.Value, User.UserGuidId());
                await _bll.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

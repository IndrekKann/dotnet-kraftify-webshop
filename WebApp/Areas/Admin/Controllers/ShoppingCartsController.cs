#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
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
            var shoppingCarts = await _bll.ShoppingCarts.GetAllAsync();
            
            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new ShoppingCartViewModel
            {
                ShoppingCart = await _bll.ShoppingCarts.FirstOrDefaultAsync(id.Value),
                Products = await _bll.ProductInLists.GetProductsForShoppingCartAsync(id.Value),
                SubTotal = await _bll.ProductInLists.GetTotalCost(id.Value)
            };

            if (vm.ShoppingCart == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: ShoppingCarts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.ShoppingCart shoppingCart)
        {
            if (ModelState.IsValid)
            {
                shoppingCart.AppUserId = User.UserGuidId();
                _bll.ShoppingCarts.Add(shoppingCart);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _bll.ShoppingCarts.FirstOrDefaultAsync(id.Value);
            
            if (shoppingCart == null)
            {
                return NotFound();
            }
            
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ShoppingCarts.UpdateAsync(shoppingCart);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(shoppingCart);
        }

        public async Task<IActionResult> PlaceOrder(Guid id)
        {
            if (id != null)
            {
                var orderId = await _bll.Orders.PlaceOrder(id, User.UserGuidId());
                await _bll.SaveChangesAsync();
                
                return RedirectToAction("Details", "Orders", new { id = orderId });
            }

            var shoppingCartId = _bll.ShoppingCarts.GetByAppUserId(User.UserGuidId()).Id.ToString();
            
            return RedirectToAction("Details", "ShoppingCarts", new { id = shoppingCartId });
        }

        public async Task<IActionResult> RemoveFromShoppingCart(Guid? id)
        {
            if (id != null)
            {
                await _bll.ProductInLists.RemoveFromShoppingCart(id.Value, User.UserGuidId());
                await _bll.SaveChangesAsync();
            }

            var shoppingCartId = _bll.ShoppingCarts.GetByAppUserId(User.UserGuidId()).Id.ToString();
            
            return RedirectToAction("Details", "ShoppingCarts", new { id = shoppingCartId });
        }

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _bll.ShoppingCarts.FirstOrDefaultAsync(id.Value);
            
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        // Principal is, shopping cart cannot be deleted, only the connections
        // hence why we use this for that
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ShoppingCarts.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

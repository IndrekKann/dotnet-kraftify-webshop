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
    public class OrdersController : Controller
    {
        private readonly IAppBLL _bll;

        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _bll.Orders.GetAllAsync();

            return View(orders.OrderByDescending(o => int.Parse(o.OrderNumber)));
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

        // GET: Orders/Create
        public IActionResult Create()
        {
            var vm = new OrderCreateEditViewModel
            {
                ShoppingCartSelectList = new SelectList(
                    _bll.ShoppingCarts.GetAllAsync().Result,
                    nameof(ShoppingCart.Id), nameof(ShoppingCart.Id)),
            };

            return View(vm);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Orders.Add(vm.Order!);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            vm.ShoppingCartSelectList = new SelectList(
                _bll.ShoppingCarts.GetAllAsync().Result, 
                nameof(ShoppingCart.Id), nameof(ShoppingCart.Id), vm.Order!.ShoppingCartId);

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

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id, OrderCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            vm.ShoppingCartSelectList = new SelectList(
                _bll.ShoppingCarts.GetAllAsync().Result, 
                nameof(ShoppingCart.Id), nameof(ShoppingCart.Id));

            vm.Order = await _bll.Orders.FirstOrDefaultAsync(id.Value);
            
            if (vm.Order == null)
            {
                return NotFound();
            }
            
            return View(vm);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OrderCreateEditViewModel vm)
        {
            if (id != vm.Order!.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Orders.UpdateAsync(vm.Order);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id, OrderCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _bll.Orders.FirstOrDefaultAsync(id.Value);
            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Orders.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

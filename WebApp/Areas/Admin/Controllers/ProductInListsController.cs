#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductInListsController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductInListsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ProductInLists
        public async Task<IActionResult> Index()
        {
            var result = await _bll.ProductInLists.GetAllAsync();

            return View(result);
        }

        // GET: ProductInLists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInList = await _bll.ProductInLists.FirstOrDefaultAsync(id.Value);

            if (productInList == null)
            {
                return NotFound();
            }

            return View(productInList);
        }

        // GET: ProductInLists/Create
        public IActionResult Create()
        {
            var vm = new ProductInListCreateEditViewModel
            {
                ShoppingCartSelectList = new SelectList(
                    _bll.ShoppingCarts.GetAllAsync().Result,
                    nameof(ShoppingCart.Id), nameof(ShoppingCart.AppUserId)), // should be email
                ProductSelectList = new SelectList(
                    _bll.Products.GetAllAsync().Result,
                    nameof(Product.Id), nameof(Product.Name))
            };

            return View(vm);
        }

        // POST: ProductInLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInListCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.ProductInLists.Add(vm.ProductInList!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.ShoppingCartSelectList = new SelectList(
                _bll.ShoppingCarts.GetAllAsync().Result, 
                nameof(ShoppingCart.Id), nameof(ShoppingCart.Id), vm.ProductInList!.ShoppingCartId);
            
            vm.ProductSelectList = new SelectList(
                _bll.Products.GetAllAsync().Result, 
                nameof(Product.Id), nameof(Product.Name), vm.ProductInList.ProductId);

            return View(vm);
        }

        // GET: ProductInLists/Edit/5
        public async Task<IActionResult> Edit(Guid? id, ProductInListCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            vm.ShoppingCartSelectList = new SelectList(
                _bll.ShoppingCarts.GetAllAsync().Result, 
                nameof(ShoppingCart.Id), nameof(ShoppingCart.Id));
            
            vm.ProductSelectList = new SelectList(
                _bll.Products.GetAllAsync().Result, 
                nameof(Product.Id), nameof(Product.Name));

            vm.ProductInList = await _bll.ProductInLists.FirstOrDefaultAsync(id.Value);
            
            if (vm.ProductInList == null)
            {
                return NotFound();
            }
            
            return View(vm);
        }

        // POST: ProductInLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductInListCreateEditViewModel vm)
        {
            if (id != vm.ProductInList!.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ProductInLists.UpdateAsync(vm.ProductInList);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: ProductInLists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInList = await _bll.ProductInLists.FirstOrDefaultAsync(id.Value);
            
            if (productInList == null)
            {
                return NotFound();
            }

            return View(productInList);
        }

        // POST: ProductInLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ProductInLists.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

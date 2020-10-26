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
    public class PricesController : Controller
    {
        private readonly IAppBLL _bll;

        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            var prices = await _bll.Prices.GetAllAsync();

            return View(prices);
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public IActionResult Create()
        {
            var vm = new PriceCreateEditViewModel
            {
                CurrencySelectList = new SelectList(
                    _bll.Currencies.GetAllAsync().Result,
                    nameof(Currency.Id), nameof(Currency.Abbreviation))
            };

            return View(vm);
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PriceCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Price!.Cost = await _bll.Prices.GetCorrectPriceCost(vm.Price!.Cost);
                _bll.Prices.Add(vm.Price!);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.CurrencySelectList = new SelectList(
                _bll.Currencies.GetAllAsync().Result, 
                nameof(Currency.Id), nameof(Currency.Abbreviation), vm.Price!.CurrencyId);
            
            return View(vm);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id, PriceCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            vm.CurrencySelectList = new SelectList(
                _bll.Currencies.GetAllAsync().Result, 
                nameof(Currency.Id), nameof(Currency.Abbreviation));

            vm.Price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            
            if (vm.Price == null)
            {
                return NotFound();
            }
            
            return View(vm);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PriceCreateEditViewModel vm)
        {
            if (id != vm.Price!.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vm.Price!.Cost = await _bll.Prices.GetCorrectPriceCost(vm.Price!.Cost);
                await _bll.Prices.UpdateAsync(vm.Price);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Prices.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CurrenciesController : Controller
    {
        private readonly IAppBLL _bll;

        public CurrenciesController(IAppBLL bll)
        {
            _bll = bll;
        }
        

        // GET: Currencies
        public async Task<IActionResult> Index()
        {
            var currencies = await _bll.Currencies.GetAllAsync();
            
            return View(currencies);
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _bll.Currencies.FirstOrDefaultAsync(id.Value);
            
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.Currency currency)
        {

            if (ModelState.IsValid)
            {
                _bll.Currencies.Add(currency);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _bll.Currencies.FirstOrDefaultAsync(id.Value);
            
            if (currency == null)
            {
                return NotFound();
            }
            
            return View(currency);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Currencies.UpdateAsync(currency);
                await _bll.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
            }
            
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _bll.Currencies.FirstOrDefaultAsync(id.Value);
            
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Currencies.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

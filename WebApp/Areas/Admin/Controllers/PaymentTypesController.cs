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
    public class PaymentTypesController : Controller
    {
        private readonly IAppBLL _bll;

        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            var paymentTypes = await _bll.PaymentTypes.GetAllAsync();

            return View(paymentTypes);
        }

        // GET: PaymentTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id.Value);
            
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                _bll.PaymentTypes.Add(paymentType);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(paymentType);
        }

        // GET: PaymentTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id.Value);
            
            if (paymentType == null)
            {
                return NotFound();
            }
            
            return View(paymentType);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.PaymentTypes.UpdateAsync(paymentType);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(paymentType);
        }

        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id.Value);
            
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.PaymentTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

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
    public class ProductTypesController : Controller
    {
        private readonly IAppBLL _bll;
        
        public ProductTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            var productTypes = await _bll.ProductTypes.GetAllAsync();

            return View(productTypes);
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id.Value);
            
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BLL.App.DTO.ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _bll.ProductTypes.Add(productType);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id.Value);
            
            if (productType == null)
            {
                return NotFound();
            }
            
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BLL.App.DTO.ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ProductTypes.UpdateAsync(productType);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(productType);

        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id.Value);
            
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);

        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ProductTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

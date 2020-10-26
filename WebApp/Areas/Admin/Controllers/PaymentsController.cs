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
    public class PaymentsController : Controller
    {
        private readonly IAppBLL _bll;

        public PaymentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = await _bll.Payments.GetAllAsync();

            return View(payments);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _bll.Payments.FirstOrDefaultAsync(id.Value);

            var vm = new ShoppingCartViewModel
            {
                Products = await _bll.ProductInLists.GetProductsForOrderAsync(payment.OrderId),
                SubTotal = await _bll.ProductInLists.GetTotalCostForOrder(payment.OrderId),
                Payment = payment
            };
            
            return View(vm);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            var vm = new PaymentCreateEditViewModel
            {
                PaymentTypeSelectList = new SelectList(
                    _bll.PaymentTypes.GetAllAsync().Result,
                    nameof(PaymentType.Id), nameof(PaymentType.Name)),
                OrderSelectList = new SelectList(
                    _bll.Orders.GetAllAsync().Result,
                    nameof(Order.Id), nameof(Order.Id)),
                DestinationSelectList = new SelectList(
                    _bll.Destinations.GetAllAsync().Result,
                    nameof(Destination.Id), nameof(Destination.Location))
            };

            return View(vm);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Payments.Add(vm.Payment!);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            vm.PaymentTypeSelectList = new SelectList(
                _bll.PaymentTypes.GetAllAsync().Result, 
                nameof(PaymentType.Id), nameof(PaymentType.Name), vm.Payment!.PaymentTypeId);
            
            vm.OrderSelectList = new SelectList(
                _bll.Orders.GetAllAsync().Result, 
                nameof(Order.Id), nameof(Order.Id), vm.Payment.OrderId);

            vm.DestinationSelectList = new SelectList(
                _bll.Destinations.GetAllAsync().Result,
                nameof(Destination.Id), nameof(Destination.Location));

            return View(vm);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(Guid? id, PaymentCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            vm.PaymentTypeSelectList = new SelectList(
                _bll.PaymentTypes.GetAllAsync().Result, 
                nameof(PaymentType.Id), nameof(PaymentType.Name));
            
            vm.OrderSelectList = new SelectList(
                _bll.Orders.GetAllAsync().Result, 
                nameof(Order.Id), nameof(Order.Id));
            
            vm.DestinationSelectList = new SelectList(
                _bll.Destinations.GetAllAsync().Result,
                nameof(Destination.Id), nameof(Destination.Location));

            vm.Payment = await _bll.Payments.FirstOrDefaultAsync(id.Value);
            
            if (vm.Payment == null)
            {
                return NotFound();
            }
            
            return View(vm);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PaymentCreateEditViewModel vm)
        {
            if (id != vm.Payment!.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Payments.UpdateAsync(vm.Payment);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _bll.Payments.FirstOrDefaultAsync(id.Value);
            
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Payments.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

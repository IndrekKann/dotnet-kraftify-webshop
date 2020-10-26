#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Domain.App.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Areas.Admin.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly IWebHostEnvironment _env; 

        public ProductsController(IAppBLL bll, IWebHostEnvironment env)
        {
            _bll = bll;
            _env = env;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? search, string? searchType, string? order, string? limit = "18", string? page = "1")
        {
            var products = await _bll.Products.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                products = await _bll.Products.GetSearchAsync(search);
            }
            else if (!string.IsNullOrWhiteSpace(searchType))
            {
                products = await _bll.Products.GetSearchTypeAsync(searchType);
            }

            if (!string.IsNullOrWhiteSpace(order))
            {
                products = await _bll.Products.GetOrderedProducts(order, products);
            }

            var vm = new ProductIndexViewModel()
            {
                Products = products.ToArray().Skip((int.Parse(page!) - 1) * int.Parse(limit!)).Take(int.Parse(page!) * int.Parse(limit!)),
                ProductTypes = await _bll.ProductTypes.GetAllAsync(),
                Pages = await _bll.Products.GeneratePagination(await _bll.Products.GetPagesAmount(limit), int.Parse(page!)),
                CurrentURL = Regex.Replace(HttpContext.Request.QueryString.ToString(), @"[&?]Page=[\d]*", string.Empty)
            };
            
            return View(vm);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new ProductIndexViewModel()
            {
                Product = await _bll.Products.FirstOrDefaultAsync(id.Value),
                ProductTypes = await _bll.ProductTypes.GetAllAsync()
            };

            if (vm.Product == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        public async Task<IActionResult> AddToShoppingCart(Guid? id)
        {
            if (id != null)
            {
                await _bll.ProductInLists.AddToShoppingCart(id.Value, User.UserGuidId());
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var vm = new ProductCreateEditViewModel()
            {
                ProductTypeSelectList = new SelectList(
                    _bll.ProductTypes.GetAllAsync().Result,
                    nameof(ProductType.Id), 
                    nameof(ProductType.Name)),

                PriceSelectList = new SelectList(
                    _bll.Prices.GetAllAsync().Result,
                    nameof(Price.Id), 
                    nameof(Price.Cost))
            };

            return View(vm);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var uniqueFileName = UploadedFile(vm);
                vm.Product!.Image = uniqueFileName;
                _bll.Products.Add(vm.Product!);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            vm.ProductTypeSelectList = new SelectList(
                _bll.ProductTypes.GetAllAsync().Result, 
                nameof(ProductType.Id), nameof(ProductType.Name), vm.Product!.ProductTypeId);
            
            vm.PriceSelectList = new SelectList(
                _bll.Prices.GetAllAsync().Result, 
                nameof(Price.Id), nameof(Price.Cost), vm.Product!.PriceId);
                        
            return View(vm);
        }
        
          
        private string? UploadedFile(ProductCreateEditViewModel vm)  
        {  
            string? uniqueFileName = null;  
  
            if (vm.Image != null)  
            {  
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");  
                uniqueFileName = Guid.NewGuid() + "_" + vm.Image!.FileName;  
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                vm.Image!.CopyTo(fileStream);
            }
            
            return uniqueFileName;  
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id, ProductCreateEditViewModel vm)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            vm.ProductTypeSelectList = new SelectList(
                _bll.ProductTypes.GetAllAsync().Result, 
                nameof(ProductType.Id), nameof(ProductType.Name));

            vm.PriceSelectList = new SelectList(
                _bll.Prices.GetAllAsync().Result, 
                nameof(Price.Id), nameof(Price.Cost));
            
            vm.Product = await _bll.Products.FirstOrDefaultAsync(id.Value);
            
            if (vm.Product == null)
            {
                return NotFound();
            }
            
            return View(vm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductCreateEditViewModel vm)
        {
            if (id != vm.Product!.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var uniqueFileName = UploadedFile(vm);
                vm.Product!.Image = uniqueFileName;
                await _bll.Products.UpdateAsync(vm.Product);
                await _bll.SaveChangesAsync();
 
                return RedirectToAction(nameof(Index));
            }
            
            return View(vm);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Products.FirstOrDefaultAsync(id.Value);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Products.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}

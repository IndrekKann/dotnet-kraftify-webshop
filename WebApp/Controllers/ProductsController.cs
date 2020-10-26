#pragma warning disable 1591
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class ProductsController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
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
        
        [Authorize]
        public async Task<IActionResult> AddToShoppingCart(Guid? id)
        {
            if (id != null)
            {
                await _bll.ProductInLists.AddToShoppingCart(id.Value, User.UserGuidId());
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

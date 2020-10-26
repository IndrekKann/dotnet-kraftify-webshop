#pragma warning disable 1998
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.webshop.indrek.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ProductService : 
        BaseEntityService<IAppUnitOfWork, IProductRepository, IProductServiceMapper, 
            DAL.App.DTO.Product, BLL.App.DTO.Product>, IProductService
    {
        public ProductService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Products, new ProductServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<ProductView>> GetAllForViewAsync(string? search, string? searchType, string? order, string? limit = "18", string? page = "1")
        {
            var allProducts = (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapProductView(e));

            if (!string.IsNullOrWhiteSpace(search))
            {
                allProducts = allProducts.Where(p => p.Name!.ToString().ToLower().Contains(search.ToLower()));
            }
            
            else if (!string.IsNullOrWhiteSpace(searchType))
            {
                allProducts = allProducts.Where(p => p.ProductTypeId.ToString() == searchType);
            }

            if (!string.IsNullOrWhiteSpace(order))
            {
                var orderedProducts = order switch
                {
                    "1" => allProducts.OrderBy(e => e.Name),
                    "2" => allProducts.OrderByDescending(e => e.Name),
                    "3" => allProducts.OrderBy(e => e.Cost),
                    "4" => allProducts.OrderByDescending(e => e.Cost),
                    _ => allProducts
                };

                allProducts = orderedProducts;
            }
            
            return allProducts.Skip((int.Parse(page!) - 1) * int.Parse(limit!)).Take(int.Parse(page!) * int.Parse(limit!));
        }

        public virtual async Task<IEnumerable<Product>> GetSearchAsync(string search)
        {
            return (await Repository.GetSearchAsync(search)).Select(e => Mapper.Map(e));
        }

        public virtual async Task<IEnumerable<Product>> GetSearchTypeAsync(string searchType)
        {
            return (await Repository.GetSearchTypeAsync(searchType)).Select(e => Mapper.Map(e));
        }
        
        public virtual async Task<IEnumerable<Product>> GetOrderedProducts(string order, IEnumerable<Product> products)
        {
            var orderedProducts = order switch
            {
                "1" => products.OrderBy(e => e.Name),
                "2" => products.OrderByDescending(e => e.Name),
                "3" => products.OrderBy(e => e.Price!.Cost),
                "4" => products.OrderByDescending(e => e.Price!.Cost),
                _ => products
            };

            return orderedProducts;
        }

        public async Task<ProductView> FirstOrDefaultViewAsync(Guid id)
        {
            return Mapper.MapProductView(await Repository.FirstOrDefaultViewAsync(id));
        }

        public virtual async Task<int> GetPagesAmount(string? limit = "18")
        {
            var productCount = Repository.GetAllAsync().Result.Count();
            var productsPerPage = limit switch
            {
                "18" => 18.0,
                "36" => 36.0,
                "72" => 72.0,
                _ => 18.0
            };
            
            return (int) Math.Ceiling(productCount / productsPerPage);
        }

        public virtual async Task<IEnumerable<string>> GeneratePagination(int totalPages, int currentPage = 1)
        {
            var pagination = new List<string>();
            if (totalPages > 3)
            {
                if (currentPage < 3)
                {
                    for (var i = 1; i <= 3; i++)
                    {
                        pagination.Add(i.ToString());
                    }
                    pagination.Add("...");
                    pagination.Add(totalPages.ToString());

                    return pagination;
                }

                if (currentPage > totalPages - 2)
                {
                    pagination.Add("1");
                    pagination.Add("...");
                    for (var i = totalPages - 2; i <= totalPages; i++)
                    {
                        pagination.Add(i.ToString());
                    }

                    return pagination;
                }

                if (currentPage <= totalPages - 2)
                {
                    pagination.Add("1");
                    pagination.Add("...");
                    for (var i = currentPage - 1; i <= currentPage + 1; i++)
                    {
                        pagination.Add(i.ToString());
                    }
                    pagination.Add("...");
                    pagination.Add(totalPages.ToString());

                    return pagination;
                }
            }
            
            for (var i = 1; i <= totalPages; i++)
            {
                pagination.Add(i.ToString());
            }

            return pagination;
        }

    }
}
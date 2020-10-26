using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.webshop.indrek.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductInList = DAL.App.DTO.ProductInList;

namespace DAL.App.EF.Repositories
{
    public class ProductInListRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.ProductInList, DAL.App.DTO.ProductInList>, 
        IProductInListRepository
    {
        public ProductInListRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.ProductInList, DTO.ProductInList>())
        {
        }
        
        public override async Task<IEnumerable<ProductInList>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(pil => pil.Product).ThenInclude(p => p!.Name).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product).ThenInclude(p => p!.Description).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product).ThenInclude(p => p!.Price);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            
            return result;
        }
        
        public async Task<IEnumerable<ProductInList>> GetProductsForShoppingCartAsync(Guid scId, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(pil => pil.Product).ThenInclude(p => p!.Name).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product).ThenInclude(p => p!.Description).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product).ThenInclude(p => p!.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations)
                .Where(pil => pil.ShoppingCartId.Equals(scId));
            
            var domainItems = await query.AsNoTracking().ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }

        public async Task<IEnumerable<ProductInList>> GetProductsForOrderAsync(Guid orderId, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            query = query
                .Include(pil => pil.Product).ThenInclude(p => p!.Name).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product).ThenInclude(p => p!.Description).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product).ThenInclude(p => p!.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations)
                .Where(pil => pil.OrderId.Equals(orderId));
            
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }
        
        public ProductInList GetPilByAppUserId(Guid id)
        {
            return Mapper.Map(PrepareQuery().FirstOrDefaultAsync(pil => pil.Id.Equals(id)).Result);
        }

        public ProductInList? ProductAlreadyInShoppingCart(Guid shoppingCartId, Guid productId)
        {
            return Mapper.Map(PrepareQuery().Where(pil => pil.ShoppingCartId.Equals(shoppingCartId) && pil.ProductId.Equals(productId)).FirstOrDefaultAsync().Result)?? null;
        }

        public async Task<IEnumerable<ProductInListView>> GetProductsForShoppingCartViewAsync(Guid scId)
        {
            return await RepoDbSet
                .Include(pil => pil.Product).ThenInclude(p => p!.Name).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product)
                .Include(pil => pil.Product).ThenInclude(p => p!.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations)
                .Where(pil => pil.ShoppingCartId.Equals(scId))
                .Select(a => new ProductInListView
                {
                    Id = a.Id,
                    Product = a.Product!.Name,
                    ProductId = a.Product!.Id,
                    Image = a.Product!.Image!,
                    Cost = a.Product!.Price!.Cost,
                    Symbol = a.Product!.Price!.Currency!.Symbol,
                    Quantity = a.Quantity,
                    TotalCost = a.TotalCost
                }
                ).ToListAsync();
        }
        
        
        public async Task<IEnumerable<ProductInListView>> GetProductsForOrderViewAsync(Guid orderId)
        {
            return await RepoDbSet
                .Include(pil => pil.Product).ThenInclude(p => p!.Name).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product)
                .Include(pil => pil.Product).ThenInclude(p => p!.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations)
                .Where(pil => pil.OrderId.Equals(orderId))
                .Select(a => new ProductInListView
                {
                    Id = a.Id,
                    Product = a.Product!.Name,
                    ProductId = a.Product!.Id,
                    Image = a.Product!.Image!,
                    Cost = a.Product!.Price!.Cost,
                    Symbol = a.Product!.Price!.Currency!.Symbol,
                    Quantity = a.Quantity,
                    TotalCost = a.TotalCost
                }
                ).ToListAsync();
        }

        public async Task<IEnumerable<ProductInListView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(pil => pil.Product)
                .Include(pil => pil.Product).ThenInclude(p => p!.Name).ThenInclude(ls => ls!.Translations)
                .Include(pil => pil.Product).ThenInclude(p => p!.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations)
                .Select(a => new ProductInListView
                {
                    Id = a.Id,
                    Product = a.Product!.Name,
                    ProductId = a.Product!.Id,
                    Image = a.Product!.Image!,
                    Cost = a.Product!.Price!.Cost,
                    Symbol = a.Product!.Price!.Currency!.Symbol,
                    Quantity = a.Quantity,
                    TotalCost = a.TotalCost
                }).ToListAsync();
        }

    }
}

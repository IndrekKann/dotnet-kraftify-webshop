#pragma warning restore 1998
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.webshop.indrek.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Product, DAL.App.DTO.Product>, 
        IProductRepository
    {
        public ProductRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.Product, DTO.Product>())
        {
        }

        public override async Task<IEnumerable<Product>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.ProductType).ThenInclude(pt => pt!.Name).ThenInclude(ls => ls!.Translations)
                .Include(p => p.Name).ThenInclude(ls => ls!.Translations)
                .Include(p => p.Description).ThenInclude(d => d!.Translations)
                .Include(p => p.Price)
                .Include(p => p.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            return result;
        }
        
        public override async Task<Product> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(p => p.Name).ThenInclude(t => t!.Translations)
                .Include(p => p.Description).ThenInclude(t => t!.Translations)
                .Include(p => p.ProductType).ThenInclude(pt => pt!.Name).ThenInclude(ls => ls!.Translations)
                .Include(p => p.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
            
            var result = Mapper.Map(domainEntity);
            return result;
        }

#pragma warning disable 1998
        public async Task<IEnumerable<Product>> GetSearchAsync(string search, object? userId = null, bool noTracking = true)
#pragma warning restore 1998
        {
            var query = RepoDbSet
                .Include(p => p.ProductType).ThenInclude(pt => pt!.Name).ThenInclude(ls => ls!.Translations)
                .Include(p => p.Name).ThenInclude(ls => ls!.Translations)
                .Include(p => p.Description).ThenInclude(d => d!.Translations)
                .Include(p => p.Price)
                .Include(p => p.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations).AsEnumerable();

            query = query
                .Where(p => p.Name!.ToString().ToLower().Contains(search.ToLower()) || search == null);
            
            var domainItems = query.ToList();
            var result = domainItems.Select(e => Mapper.Map(e));
            
            return result;
        }

        public async Task<IEnumerable<Product>> GetSearchTypeAsync(string searchType, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.ProductType).ThenInclude(pt => pt!.Name).ThenInclude(ls => ls!.Translations)
                .Include(p => p.Name).ThenInclude(ls => ls!.Translations)
                .Include(p => p.Description).ThenInclude(d => d!.Translations)
                .Include(p => p.Price)
                .Include(p => p.Price).ThenInclude(c => c!.Currency).ThenInclude(s => s!.Symbol).ThenInclude(ls => ls!.Translations);

            query = query
                .Where(p => p.ProductTypeId.ToString() == searchType || searchType == null);
            
            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            
            return result;
        }

        public async Task<IEnumerable<ProductView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(a => a.ProductType).ThenInclude(a => a!.Name).ThenInclude(a => a!.Translations)
                .Include(a => a.Description).ThenInclude(a => a!.Translations)
                .Include(a => a.Price).ThenInclude(a => a!.Currency).ThenInclude(a => a!.Symbol).ThenInclude(a => a!.Translations)
                .Include(a => a.Name).ThenInclude(a => a!.Translations)
                .Include(a => a.Description).ThenInclude(a => a!.Translations)
                .Select(a => new ProductView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ProductType = a.ProductType!.Name,
                    ProductTypeId = a.ProductTypeId,
                    Cost = a.Price!.Cost,
                    Symbol = a.Price!.Currency!.Symbol,
                    Image = a.Image
                }
                ).ToListAsync();
        }
        
        public async Task<ProductView> FirstOrDefaultViewAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            return await RepoDbSet
                .Include(a => a.ProductType).ThenInclude(a => a!.Name).ThenInclude(a => a!.Translations)
                .Include(a => a.Description).ThenInclude(a => a!.Translations)
                .Include(a => a.Price).ThenInclude(a => a!.Currency).ThenInclude(a => a!.Symbol).ThenInclude(a => a!.Translations)
                .Include(a => a.Name).ThenInclude(a => a!.Translations)
                .Include(a => a.Description).ThenInclude(a => a!.Translations).Where(a => a.Id.Equals(id))
                .Select(a => new ProductView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ProductType = a.ProductType!.Name,
                    ProductTypeId = a.ProductTypeId,
                    Cost = a.Price!.Cost,
                    Symbol = a.Price!.Currency!.Symbol,
                    Image = a.Image
                })
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

    }
}
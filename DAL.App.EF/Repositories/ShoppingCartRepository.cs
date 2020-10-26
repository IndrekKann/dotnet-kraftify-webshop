using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.webshop.indrek.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using ShoppingCart = DAL.App.DTO.ShoppingCart;

namespace DAL.App.EF.Repositories
{
    public class ShoppingCartRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.ShoppingCart, DAL.App.DTO.ShoppingCart>, 
        IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.ShoppingCart, DTO.ShoppingCart>())
        {
        }

        public override async Task<IEnumerable<ShoppingCart>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            
            query = query
                .Include(sc => sc.AppUser);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            
            return result;
        }

        public ShoppingCart GetByAppUserId(Guid userId)
        {
            var query = PrepareQuery().Where(sc => sc.AppUserId == userId);
            return Mapper.Map(query.FirstOrDefault(sc => sc.AppUserId == userId));
        }
        
    }
}

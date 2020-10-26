using System;
using DAL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>, IShoppingCartRepositoryCustom
    {
        //Task<IEnumerable<ShoppingCart>> GetProductsForShoppingCartAsync(Guid scId, object? userId = null, bool noTracking = true);
        ShoppingCart GetByAppUserId(Guid userId);
    }

}
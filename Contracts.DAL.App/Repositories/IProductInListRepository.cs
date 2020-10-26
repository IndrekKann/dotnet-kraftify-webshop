using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductInListRepository : IBaseRepository<ProductInList>, IProductInListRepositoryCustom
    {
        Task<IEnumerable<ProductInList>> GetProductsForShoppingCartAsync(Guid scId, object? userId = null, bool noTracking = true);
        Task<IEnumerable<ProductInList>> GetProductsForOrderAsync(Guid orderId, object? userId = null, bool noTracking = true);

        ProductInList GetPilByAppUserId(Guid id);
        ProductInList? ProductAlreadyInShoppingCart(Guid shoppingCartId, Guid productId);
        Task<IEnumerable<ProductInListView>> GetProductsForShoppingCartViewAsync(Guid scId);
        Task<IEnumerable<ProductInListView>> GetAllForViewAsync();
        Task<IEnumerable<ProductInListView>> GetProductsForOrderViewAsync(Guid orderId);
    }

}
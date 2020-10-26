using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IProductInListService : IBaseEntityService<ProductInList>, IProductInListRepositoryCustom<ProductInList>
    {
        Task<IEnumerable<ProductInList>> GetProductsForShoppingCartAsync(Guid id);
        Task<double> GetTotalCost(Guid scId);
        Task AddToShoppingCart(Guid id, Guid userId, int quantity = 1);
        Task RemoveFromShoppingCart(Guid id, Guid userId);
        Task<IEnumerable<ProductInList>> GetProductsForOrderAsync(Guid orderId);
        Task<double> GetTotalCostForOrder(Guid id);
        Task<IEnumerable<ProductInListView>> GetProductsForShoppingCartViewAsync(Guid scId);
        Task<IEnumerable<ProductInListView>> GetAllForViewAsync();
        Task AddToShoppingCartApi(Guid productId, Guid scId, int quantity = 1);
        Task DecreaseQuantity(ProductInList pil);
        Task<IEnumerable<ProductInListView>> GetProductsForOrderViewAsync(Guid paymentOrderId);
    }
    
}
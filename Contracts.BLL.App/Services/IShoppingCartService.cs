using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IShoppingCartService : IBaseEntityService<ShoppingCart>, IShoppingCartRepositoryCustom<ShoppingCart>
    {
        ShoppingCart GetByAppUserId(Guid userId);
        Task ClearShoppingCart(Guid scId);
    }
    
}

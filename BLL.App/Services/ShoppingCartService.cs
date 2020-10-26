using System;
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
    public class ShoppingCartService : 
        BaseEntityService<IAppUnitOfWork, IShoppingCartRepository, IShoppingCartServiceMapper, 
            DAL.App.DTO.ShoppingCart, BLL.App.DTO.ShoppingCart>, IShoppingCartService
    {
        public ShoppingCartService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ShoppingCarts, new ShoppingCartServiceMapper())
        {
        }

        public ShoppingCart GetByAppUserId(Guid userId)
        {
            return Mapper.Map(UOW.ShoppingCarts.GetByAppUserId(userId));
        }

        public async Task ClearShoppingCart(Guid scId)
        {
            foreach (var productInList in UOW.ProductInLists.GetProductsForShoppingCartAsync(scId).Result)
            {
                await UOW.ProductInLists.RemoveAsync(productInList);
            }
            
            await UOW.SaveChangesAsync();
        }
        
    }
}

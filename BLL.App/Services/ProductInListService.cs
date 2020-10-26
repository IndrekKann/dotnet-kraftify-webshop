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
using Product = DAL.App.DTO.Product;

namespace BLL.App.Services
{
    public class ProductInListService :
        BaseEntityService<IAppUnitOfWork, IProductInListRepository, IProductInListServiceMapper,
            DAL.App.DTO.ProductInList, BLL.App.DTO.ProductInList>, IProductInListService
    {
        public ProductInListService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ProductInLists,
            new ProductInListServiceMapper())
        {
        }

        public async Task<IEnumerable<ProductInList>> GetProductsForShoppingCartAsync(Guid scId)
        {
            return (await Repository.GetProductsForShoppingCartAsync(scId)).Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<ProductInListView>> GetProductsForShoppingCartViewAsync(Guid scId)
        {
            return (await Repository.GetProductsForShoppingCartViewAsync(scId)).Select(e => Mapper.MapProductInListView(e));
        }

        public async Task<IEnumerable<ProductInListView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapProductInListView(e));
        }

        public async Task<IEnumerable<ProductInList>> GetProductsForOrderAsync(Guid orderId)
        {
            return (await Repository.GetProductsForOrderAsync(orderId)).Select(e => Mapper.Map(e));
        }
        
        public async Task<IEnumerable<ProductInListView>> GetProductsForOrderViewAsync(Guid orderId)
        {
            return (await Repository.GetProductsForOrderViewAsync(orderId)).Select(e => Mapper.MapProductInListView(e));
        }

#pragma warning disable 1998
        public async Task<double> GetTotalCost(Guid scId)
#pragma warning restore 1998
        {
            return Math.Round(Repository.GetProductsForShoppingCartAsync(scId).Result.Sum(a => a.TotalCost), 2, MidpointRounding.ToEven);
        }

#pragma warning disable 1998
        public async Task<double> GetTotalCostForOrder(Guid orderId)
#pragma warning restore 1998
        {
            return Math.Round(Repository.GetProductsForOrderAsync(orderId).Result.Sum(a => a.TotalCost), 2, MidpointRounding.ToEven);
        }

        public async Task AddToShoppingCart(Guid id, Guid userId, int quantity = 1)
        {
            var shoppingCart = UOW.ShoppingCarts.GetByAppUserId(userId);
            var product = UOW.Products.FirstOrDefaultAsync(id).Result;
            
            var pil = UOW.ProductInLists.ProductAlreadyInShoppingCart(shoppingCart.Id, product.Id);

            if (pil == null)
            {
                var connection = new ProductInList()
                {
                    Id = Guid.NewGuid(),
                    ProductId = id,
                    Quantity = quantity,
                    TotalCost = product.Price!.Cost,
                    ShoppingCartId = shoppingCart.Id
                };
                
                UOW.ProductInLists.Add(Mapper.Map(connection));
                await UOW.SaveChangesAsync();                
            }
            else
            {
                pil.Quantity++;
                pil.TotalCost = product.Price!.Cost * pil.Quantity;
                await UOW.ProductInLists.UpdateAsync(pil);
                await UOW.SaveChangesAsync();
            }

        }

        public async Task AddToShoppingCartApi(Guid productId, Guid scId, int quantity = 1)
        {
            var product = UOW.Products.FirstOrDefaultAsync(productId).Result;
            
            var pil = UOW.ProductInLists.ProductAlreadyInShoppingCart(scId, productId);

            if (pil == null)
            {
                var connection = new ProductInList()
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    Quantity = quantity,
                    TotalCost = product.Price!.Cost,
                    ShoppingCartId = scId
                };
                
                UOW.ProductInLists.Add(Mapper.Map(connection));
                await UOW.SaveChangesAsync();                
            }
            else
            {
                pil.Quantity++;
                pil.TotalCost = product.Price!.Cost * pil.Quantity;
                await UOW.ProductInLists.UpdateAsync(pil);
                await UOW.SaveChangesAsync();
            }

        }

        public async Task DecreaseQuantity(ProductInList pil)
        {
            var dalPil = await UOW.ProductInLists.FirstOrDefaultAsync(pil.Id);
            var productPrice = dalPil.TotalCost / dalPil.Quantity;
            dalPil.TotalCost = Math.Round(dalPil.TotalCost - productPrice,  2, MidpointRounding.ToEven);
            dalPil.Quantity--;
            await UOW.ProductInLists.UpdateAsync(dalPil);

        }

        public async Task RemoveFromShoppingCart(Guid id, Guid userId)
        {
            var connection = UOW.ProductInLists.GetPilByAppUserId(id);
            var shoppingCart = UOW.ShoppingCarts.GetByAppUserId(userId);

            if (shoppingCart.Id.Equals(connection.ShoppingCartId))
            {
                await UOW.ProductInLists.RemoveAsync(id);
            }

        }
        
    }
}
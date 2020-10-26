using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using ee.itcollege.webshop.indrek.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using Order = BLL.App.DTO.Order;
using PaymentCreate = PublicApi.DTO.v1.PaymentCreate;

namespace BLL.App.Services
{
    public class OrderService : 
        BaseEntityService<IAppUnitOfWork, IOrderRepository, IOrderServiceMapper, 
            DAL.App.DTO.Order, BLL.App.DTO.Order>, IOrderService
    {
        public OrderService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Orders, new OrderServiceMapper())
        {
        }

        public async Task<Guid?> PlaceOrder(Guid id, Guid userId)
        {
            if (UOW.ShoppingCarts.GetByAppUserId(userId).Id.Equals(id))
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    ShoppingCartId = id,
                    AppUserId = userId,
                    Date = DateTime.Now,
                    OrderNumber = GetOrderNumber(),
                    TotalCost = Math.Round(
                        UOW.ProductInLists.GetProductsForShoppingCartAsync(
                            UOW.ShoppingCarts.GetByAppUserId(userId).Id).Result.Sum(a => a.TotalCost), 
                        2, 
                        MidpointRounding.ToEven)
                };
            
                UOW.Orders.Add(Mapper.Map(order));
                await UOW.SaveChangesAsync();

                return order.Id;
            }

            return null;
        }
        
        public async Task<Guid> PlaceOrderForApi(PaymentCreate paymentCreate)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                ShoppingCartId = paymentCreate.ShoppingCartId,
                AppUserId = paymentCreate.AppUserId,
                Date = DateTime.Now,
                OrderNumber = GetOrderNumber(),
                TotalCost = Math.Round(
                    UOW.ProductInLists.GetProductsForShoppingCartAsync(
                        paymentCreate.ShoppingCartId).Result.Sum(a => a.TotalCost), 
                    2, 
                    MidpointRounding.ToEven)
            };
            
            UOW.Orders.Add(Mapper.Map(order));
            await UOW.SaveChangesAsync();

            return order.Id;
        }

        public async Task CopyShoppingCart(Guid scId, Guid orderId)
        {
            foreach (var productInList in UOW.ProductInLists.GetProductsForShoppingCartAsync(scId).Result)
            {
                UOW.ProductInLists.Add(new ProductInList
                {
                    Id = Guid.NewGuid(),
                    ProductId = productInList.ProductId,
                    OrderId = orderId,
                    Quantity = productInList.Quantity,
                    TotalCost = productInList.TotalCost
                });
            }

            await UOW.SaveChangesAsync();
        }

        public Order GetLatestOrder(Guid appUserId)
        {
            return Mapper.Map(UOW.Orders.GetOrderByAppUserId(appUserId));
        }

        private string GetOrderNumber()
        {
            return (UOW.Orders.GetAllAsync().Result.Count() + 1).ToString();
        }
    }
}
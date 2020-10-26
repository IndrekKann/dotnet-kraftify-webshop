using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PaymentCreate = PublicApi.DTO.v1.PaymentCreate;

namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<Order>, IOrderRepositoryCustom<Order>
    {
        Task<Guid?> PlaceOrder(Guid id, Guid userId);
        Task CopyShoppingCart(Guid scId, Guid orderId);
        Order GetLatestOrder(Guid appUserId);
        Task<Guid> PlaceOrderForApi(PaymentCreate paymentCreate);
    }
    
}
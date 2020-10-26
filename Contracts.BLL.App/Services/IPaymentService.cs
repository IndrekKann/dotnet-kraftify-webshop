using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PaymentCreate = PublicApi.DTO.v1.PaymentCreate;

namespace Contracts.BLL.App.Services
{
    public interface IPaymentService : IBaseEntityService<Payment>, IPaymentRepositoryCustom<Payment>
    {
        Task MakePayment(Guid paymentTypeId, Guid destinationId, Guid orderId, Guid userId);
        Task<IEnumerable<PaymentView>> GetAllForViewAsync();
        Task<PaymentView> FirstOrDefaultForViewAsync(Guid id);
        Task MakePaymentForApi(PaymentCreate paymentCreate, Guid orderId);
    }
    
}
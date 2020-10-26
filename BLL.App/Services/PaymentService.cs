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
using PaymentCreate = PublicApi.DTO.v1.PaymentCreate;

namespace BLL.App.Services
{
    public class PaymentService : 
        BaseEntityService<IAppUnitOfWork, IPaymentRepository, IPaymentServiceMapper, 
            DAL.App.DTO.Payment, BLL.App.DTO.Payment>, IPaymentService
    {
        public PaymentService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Payments, new PaymentServiceMapper())
        {
        }

        public async Task MakePayment(Guid paymentTypeId, Guid destinationId, Guid orderId, Guid userId)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                PaymentTypeId = paymentTypeId,
                DestinationId = destinationId,
                OrderId = orderId,
                AppUserId = userId
            };
            
            UOW.Payments.Add(Mapper.Map(payment));
            await UOW.SaveChangesAsync();
        }
        
        public async Task MakePaymentForApi(PaymentCreate paymentCreate, Guid orderId)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                PaymentTypeId = paymentCreate.PaymentTypeId,
                DestinationId = paymentCreate.DestinationId,
                OrderId = orderId,
                AppUserId = paymentCreate.AppUserId
            };
            
            UOW.Payments.Add(Mapper.Map(payment));
            await UOW.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync())
                .Select(e => Mapper.MapPaymentView(e))
                .OrderByDescending(o => o.OrderNumber);
        }

        public async Task<PaymentView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapPaymentView(await Repository.FirstOrDefaultForViewAsync(id));
        }

    }
}
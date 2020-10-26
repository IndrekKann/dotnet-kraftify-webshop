using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>, IPaymentRepositoryCustom
    {
        Task<IEnumerable<PaymentView>> GetAllForViewAsync();
        Task<PaymentView> FirstOrDefaultForViewAsync(Guid id);
    }

}
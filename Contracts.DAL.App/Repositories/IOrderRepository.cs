using System;
using DAL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>, IOrderRepositoryCustom
    {
        Order GetOrderByAppUserId(Guid appUserId);
    }
    
}
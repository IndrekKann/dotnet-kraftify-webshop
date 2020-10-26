using System;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.webshop.indrek.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Order = DAL.App.DTO.Order;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Order, DAL.App.DTO.Order>, 
        IOrderRepository
    {
        public OrderRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.Order, DTO.Order>())
        {
        }
        
        public Order GetOrderByAppUserId(Guid appUserId)
        {
            return Mapper.Map(PrepareQuery().FirstOrDefaultAsync(o => o.AppUserId.Equals(appUserId)).Result);
        }

    }
}

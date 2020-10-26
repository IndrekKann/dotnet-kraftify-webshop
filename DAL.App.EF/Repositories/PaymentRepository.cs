using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.webshop.indrek.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaymentRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Payment, DAL.App.DTO.Payment>, 
        IPaymentRepository
    {
        public PaymentRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.Payment, DTO.Payment>())
        {
        }
        
        public override async Task<IEnumerable<Payment>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            
            query = query
                .Include(o => o.Order)
                .Include(pt => pt.PaymentType).ThenInclude(ls => ls!.Name!.Translations)
                .Include(d => d.Destination)
                .Include(au => au.AppUser)
                .OrderByDescending(a => a.Date);

            var domainItems = await query.ToListAsync();
            var result = domainItems.Select(e => Mapper.Map(e));
            
            return result;
        }
        
                
        public override async Task<Payment> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(p => p.Destination)
                .Include(p => p.Order)
                .Include(p => p.PaymentType).ThenInclude(ls => ls!.Name!.Translations)
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
            
            var result = Mapper.Map(domainEntity);
            
            return result;
        }

        public async Task<IEnumerable<PaymentView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(p => p.Order)
                .Include(p => p.Destination)
                .Include(p => p.PaymentType).ThenInclude(p => p!.Name).ThenInclude(p => p!.Translations)
                .Select(a => new PaymentView
                {
                    Id = a.Id,
                    Email = a.AppUser!.Email,
                    FirstName = a.AppUser!.FirstName,
                    LastName = a.AppUser!.LastName,
                    Phone = a.AppUser!.Phone,
                    OrderNumber = a.Order!.OrderNumber,
                    PaymentType = a.PaymentType!.Name,
                    Location = a.Destination!.Location,
                    Date = a.Date
                }
                ).ToListAsync();
        }

        public async Task<PaymentView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(p => p.Order)
                .Include(p => p.Destination)
                .Include(p => p.PaymentType).ThenInclude(p => p!.Name).ThenInclude(p => p!.Translations)
                .Select(a => new PaymentView
                {
                    Id = a.Id,
                    Email = a.AppUser!.Email,
                    FirstName = a.AppUser!.FirstName,
                    LastName = a.AppUser!.LastName,
                    Phone = a.AppUser!.Phone,
                    OrderNumber = a.Order!.OrderNumber,
                    PaymentType = a.PaymentType!.Name,
                    Location = a.Destination!.Location,
                    Date = a.Date
                }).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }
    }
}

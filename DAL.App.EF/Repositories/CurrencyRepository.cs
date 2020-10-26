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
    public class CurrencyRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Currency, DAL.App.DTO.Currency>, 
        ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.Currency, DTO.Currency>())
        {
        }
        
        public override async Task<IEnumerable<Currency>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.Name)
                .ThenInclude(t => t!.Translations)
                .Include(c => c.Abbreviation)
                .ThenInclude(t => t!.Translations)
                .Include(c => c.Symbol)
                .ThenInclude(t => t!.Translations);
            
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

    }
}
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
    public class ProductTypeRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.ProductType, DAL.App.DTO.ProductType>, 
        IProductTypeRepository
    {
        public ProductTypeRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.ProductType, DTO.ProductType>())
        {
        }

        public override async Task<IEnumerable<ProductType>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(l => l.Name)
                .ThenInclude(t => t!.Translations);
            
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<ProductType> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query
                .Include(l => l.Name)
                .ThenInclude(t => t!.Translations)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            
            var result = Mapper.Map(domainEntity);
            return result;
        }


        public override async Task<ProductType> UpdateAsync(ProductType entity, object? userId = null)
        {
            var domainEntity = Mapper.Map(entity);
            // fix the language string - from mapper we get new ones - so duplicate values will be created in db
            // load back from db the originals 
            domainEntity.Name = await RepoDbContext.LangStrs.Include(t => t.Translations).FirstAsync(s => s.Id == domainEntity.NameId);
            domainEntity.Name.SetTranslation(entity.Name);
            await CheckDomainEntityOwnership(domainEntity, userId);
            var trackedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
            var result = Mapper.Map(trackedDomainEntity);
            return result;        
        }

    }
}
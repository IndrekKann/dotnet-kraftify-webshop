using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.webshop.indrek.DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class PriceRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Price, DAL.App.DTO.Price>, 
        IPriceRepository
    {
        public PriceRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.Price, DTO.Price>())
        {
        }

    }
}
using Contracts.DAL.App.Repositories;
using ee.itcollege.webshop.indrek.DAL.Base.EF.Repositories;
using DAL.App.EF.Mappers;

namespace DAL.App.EF.Repositories
{
    public class DestinationRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Destination, DAL.App.DTO.Destination>, 
        IDestinationRepository
    {
        public DestinationRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.Destination, DTO.Destination>())
        {
        }
        
    }
}
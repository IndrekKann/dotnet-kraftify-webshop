using DAL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IDestinationRepository : IBaseRepository<Destination>, IDestinationRepositoryCustom
    {
    }
    
}

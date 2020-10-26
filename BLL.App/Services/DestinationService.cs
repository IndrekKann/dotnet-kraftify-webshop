using BLL.App.Mappers;
using ee.itcollege.webshop.indrek.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class DestinationService : 
        BaseEntityService<IAppUnitOfWork, IDestinationRepository, IDestinationServiceMapper, 
            DAL.App.DTO.Destination, BLL.App.DTO.Destination>, IDestinationService
    {
        public DestinationService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Destinations, new DestinationServiceMapper())
        {
        }
        
    }
}
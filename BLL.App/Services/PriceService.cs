using System.Threading.Tasks;
using BLL.App.Mappers;
using ee.itcollege.webshop.indrek.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PriceService : 
        BaseEntityService<IAppUnitOfWork, IPriceRepository, IPriceServiceMapper, 
            DAL.App.DTO.Price, BLL.App.DTO.Price>, IPriceService
    {
        public PriceService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.Prices, new PriceServiceMapper())
        {
        }

#pragma warning disable 1998
        public virtual async Task<double> GetCorrectPriceCost(double priceInCents)
#pragma warning restore 1998
        {
            return priceInCents / 100.00;
        }
    }
}
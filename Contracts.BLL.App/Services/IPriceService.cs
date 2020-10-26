using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IPriceService : IBaseEntityService<Price>, IPriceRepositoryCustom<Price>
    {
        Task<double> GetCorrectPriceCost(double priceInCents);
    }
    
}
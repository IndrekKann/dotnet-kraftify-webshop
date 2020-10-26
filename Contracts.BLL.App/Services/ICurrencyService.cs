using BLL.App.DTO;
using Contracts.DAL.App.Repositories;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Services;

namespace Contracts.BLL.App.Services
{
    public interface ICurrencyService : IBaseEntityService<Currency>, ICurrencyRepositoryCustom<Currency>
    {
    }
    
}

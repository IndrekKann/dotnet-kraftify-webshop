using BLL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IProductTypeService : IBaseEntityService<ProductType>, IProductTypeRepositoryCustom<ProductType>
    {

    }
    
}
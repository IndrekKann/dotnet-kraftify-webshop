using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.webshop.indrek.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ProductTypeService : 
        BaseEntityService<IAppUnitOfWork, IProductTypeRepository, IProductTypeServiceMapper, 
            DAL.App.DTO.ProductType, BLL.App.DTO.ProductType>, IProductTypeService
    {
        public ProductTypeService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ProductTypes, new ProductTypeServiceMapper())
        {
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.Mappers;
using ee.itcollege.webshop.indrek.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class PaymentTypeService : 
        BaseEntityService<IAppUnitOfWork, IPaymentTypeRepository, IPaymentTypeServiceMapper, 
            DAL.App.DTO.PaymentType, BLL.App.DTO.PaymentType>, IPaymentTypeService
    {
        public PaymentTypeService(IAppUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.PaymentTypes, new PaymentTypeServiceMapper())
        {
        }
        
    }
}
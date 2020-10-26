using AutoMapper;
using ee.itcollege.webshop.indrek.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class PaymentServiceMapper : BaseMapper<DALAppDTO.Payment, BLLAppDTO.Payment>, IPaymentServiceMapper
    {
        public PaymentServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Order, BLLAppDTO.Order>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ShoppingCart, BLLAppDTO.ShoppingCart>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PaymentType, BLLAppDTO.PaymentType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Destination, BLLAppDTO.Destination>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PaymentView, BLLAppDTO.PaymentView>();

            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.PaymentView MapPaymentView(DALAppDTO.PaymentView inObject)
        {
            return Mapper.Map<BLLAppDTO.PaymentView>(inObject);
        }
        
    }
}
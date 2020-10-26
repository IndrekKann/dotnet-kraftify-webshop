using AutoMapper;
using ee.itcollege.webshop.indrek.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class OrderServiceMapper : BaseMapper<DALAppDTO.Order, BLLAppDTO.Order>, IOrderServiceMapper
    {
        public OrderServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ShoppingCart, BLLAppDTO.ShoppingCart>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Currency, BLLAppDTO.Currency>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Price, BLLAppDTO.Price>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}
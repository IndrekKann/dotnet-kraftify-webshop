using AutoMapper;
using ee.itcollege.webshop.indrek.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ShoppingCartServiceMapper : BaseMapper<DALAppDTO.ShoppingCart, BLLAppDTO.ShoppingCart>, IShoppingCartServiceMapper
    {
        public ShoppingCartServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ShoppingCart, BLLAppDTO.ShoppingCart>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductInList, BLLAppDTO.ProductInList>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}

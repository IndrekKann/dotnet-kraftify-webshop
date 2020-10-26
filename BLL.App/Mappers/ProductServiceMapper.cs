using AutoMapper;
using ee.itcollege.webshop.indrek.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProductServiceMapper : BaseMapper<DALAppDTO.Product, BLLAppDTO.Product>, IProductServiceMapper
    {
        public ProductServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductView, BLLAppDTO.ProductView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductType, BLLAppDTO.ProductType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Currency, BLLAppDTO.Currency>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Price, BLLAppDTO.Price>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.ProductView MapProductView(DALAppDTO.ProductView inObject)
        {
            return Mapper.Map<BLLAppDTO.ProductView>(inObject);
        }

    }
}

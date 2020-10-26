using AutoMapper;
using ee.itcollege.webshop.indrek.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProductInListServiceMapper : BaseMapper<DALAppDTO.ProductInList, BLLAppDTO.ProductInList>, IProductInListServiceMapper
    {
        public ProductInListServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.ShoppingCart, BLLAppDTO.ShoppingCart>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Product, BLLAppDTO.Product>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductInList, BLLAppDTO.ProductInList>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductInListView, BLLAppDTO.ProductInListView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ProductType, BLLAppDTO.ProductType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Currency, BLLAppDTO.Currency>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Price, BLLAppDTO.Price>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public BLLAppDTO.ProductInListView MapProductInListView(DALAppDTO.ProductInListView inObject)
        {
            return Mapper.Map<BLLAppDTO.ProductInListView>(inObject);
        }
    }
}

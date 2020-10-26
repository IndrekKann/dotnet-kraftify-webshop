using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ProductMapper : BaseMapper<BLL.App.DTO.Product, Product>
    {
        public ProductMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ProductView, ProductView>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public ProductView MapProductView(BLL.App.DTO.ProductView inObject)
        {
            return Mapper.Map<ProductView>(inObject);
        }

    }
}
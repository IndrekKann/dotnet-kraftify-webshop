using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ProductInListMapper : BaseMapper<BLL.App.DTO.ProductInList, ProductInList>
    {
        public ProductInListMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ProductInListView, ProductInListView>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public ProductInListView MapProductInListView(BLL.App.DTO.ProductInListView inObject)
        {
            return Mapper.Map<ProductInListView>(inObject);
        }
        
    }
}
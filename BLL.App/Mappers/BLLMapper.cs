using AutoMapper;
using ee.itcollege.webshop.indrek.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, BLL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Currency, BLL.App.DTO.Currency>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Currency, DAL.App.DTO.Currency>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Order, BLL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Order, DAL.App.DTO.Order>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Payment, BLL.App.DTO.Payment>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Payment, DAL.App.DTO.Payment>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.PaymentType, BLL.App.DTO.PaymentType>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PaymentType, DAL.App.DTO.PaymentType>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Price, BLL.App.DTO.Price>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Price, DAL.App.DTO.Price>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Product, BLL.App.DTO.Product>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Product, DAL.App.DTO.Product>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ProductType, BLL.App.DTO.ProductType>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ProductType, DAL.App.DTO.ProductType>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ProductInList, BLL.App.DTO.ProductInList>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ProductInList, DAL.App.DTO.ProductInList>();
            
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ShoppingCart, BLL.App.DTO.ShoppingCart>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ShoppingCart, DAL.App.DTO.ShoppingCart>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
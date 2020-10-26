using AutoMapper;
using ee.itcollege.webshop.indrek.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            MapperConfigurationExpression.CreateMap<Domain.App.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.Currency, DAL.App.DTO.Currency>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Currency, Domain.App.Currency>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.Order, DAL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Order, Domain.App.Order>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.Payment, DAL.App.DTO.Payment>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Payment, Domain.App.Payment>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.PaymentType, DAL.App.DTO.PaymentType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.PaymentType, Domain.App.PaymentType>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.Destination, DAL.App.DTO.Destination>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Destination, Domain.App.Destination>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.Price, DAL.App.DTO.Price>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Price, Domain.App.Price>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.Product, DAL.App.DTO.Product>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Product, Domain.App.Product>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.ProductType, DAL.App.DTO.ProductType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ProductType, Domain.App.ProductType>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.ProductInList, DAL.App.DTO.ProductInList>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ProductInList, Domain.App.ProductInList>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.ShoppingCart, DAL.App.DTO.ShoppingCart>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.ShoppingCart, Domain.App.ShoppingCart>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }

}
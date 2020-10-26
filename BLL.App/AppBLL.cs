using System;
using System.Threading.Tasks;
using BLL.App.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using ee.itcollege.webshop.indrek.BLL.Base;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IShoppingCartService ShoppingCarts => 
            GetService<IShoppingCartService>(() => new ShoppingCartService(UnitOfWork));

        public ICurrencyService Currencies => 
            GetService<ICurrencyService>(() => new CurrencyService(UnitOfWork));
        
        public ILangStrService LangStrs =>
            GetService<ILangStrService>(() => new LangStrService(UnitOfWork));

        public ILangStrTranslationService LangStrTranslations =>
            GetService<ILangStrTranslationService>(() => new LangStrTranslationService(UnitOfWork));

        public IOrderService Orders  => 
            GetService<IOrderService>(() => new OrderService(UnitOfWork));

        public IPaymentService Payments => 
            GetService<IPaymentService>(() => new PaymentService(UnitOfWork));

        public IPaymentTypeService PaymentTypes => 
            GetService<IPaymentTypeService>(() => new PaymentTypeService(UnitOfWork));

        public IDestinationService Destinations => 
            GetService<IDestinationService>(() => new DestinationService(UnitOfWork));

        public IPriceService Prices => 
            GetService<IPriceService>(() => new PriceService(UnitOfWork));

        public IProductInListService ProductInLists => 
            GetService<IProductInListService>(() => new ProductInListService(UnitOfWork));

        public IProductService Products => 
            GetService<IProductService>(() => new ProductService(UnitOfWork));

        public IProductTypeService ProductTypes => 
            GetService<IProductTypeService>(() => new ProductTypeService(UnitOfWork));

    }
}

using Contracts.DAL.App.Repositories;
using ee.itcollege.webshop.indrek.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IShoppingCartRepository ShoppingCarts { get; }
        ICurrencyRepository Currencies { get; }
        ILangStrRepository LangStrs { get; }
        ILangStrTranslationRepository LangStrTranslations { get; }
        IOrderRepository Orders { get; }
        IPaymentRepository Payments { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        IDestinationRepository Destinations { get; }
        IPriceRepository Prices { get; }
        IProductInListRepository ProductInLists { get; }
        IProductRepository Products { get; }
        IProductTypeRepository ProductTypes { get; }
        
    }
}
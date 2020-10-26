using Contracts.BLL.App.Services;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public ICurrencyService Currencies { get; }
        public IOrderService Orders { get; }
        public IPaymentService Payments { get; }
        public IPaymentTypeService PaymentTypes { get; }
        public IDestinationService Destinations { get; }
        public IPriceService Prices { get; }
        public IProductInListService ProductInLists { get; }
        public IProductService Products { get; }
        public IProductTypeService ProductTypes { get; }
        public IShoppingCartService ShoppingCarts  { get; }
    }
}

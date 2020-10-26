using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.webshop.indrek.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        
        public IShoppingCartRepository ShoppingCarts =>
            GetRepository<IShoppingCartRepository>(() => new ShoppingCartRepository(UOWDbContext));

        public ICurrencyRepository Currencies =>
            GetRepository<ICurrencyRepository>(() => new CurrencyRepository(UOWDbContext));

        public ILangStrRepository LangStrs =>
            GetRepository<ILangStrRepository>(() => new LangStrRepository(UOWDbContext));

        public ILangStrTranslationRepository LangStrTranslations =>
            GetRepository<ILangStrTranslationRepository>(() => new LangStrTranslationRepository(UOWDbContext));
        
        public IOrderRepository Orders =>
            GetRepository<IOrderRepository>(() => new OrderRepository(UOWDbContext));

        public IPaymentRepository Payments =>
            GetRepository<IPaymentRepository>(() => new PaymentRepository(UOWDbContext));

        public IPaymentTypeRepository PaymentTypes =>
            GetRepository<IPaymentTypeRepository>(() => new PaymentTypeRepository(UOWDbContext));

        public IDestinationRepository Destinations =>
            GetRepository<IDestinationRepository>(() => new DestinationRepository(UOWDbContext));

        public IPriceRepository Prices =>
            GetRepository<IPriceRepository>(() => new PriceRepository(UOWDbContext));

        public IProductInListRepository ProductInLists =>
            GetRepository<IProductInListRepository>(() => new ProductInListRepository(UOWDbContext));

        public IProductRepository Products =>
            GetRepository<IProductRepository>(() => new ProductRepository(UOWDbContext));

        public IProductTypeRepository ProductTypes =>
            GetRepository<IProductTypeRepository>(() => new ProductTypeRepository(UOWDbContext));
    }
}

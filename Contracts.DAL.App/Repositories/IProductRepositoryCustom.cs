using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductRepositoryCustom : IProductRepositoryCustom<ProductView>
    {
    }

    public interface IProductRepositoryCustom<TProductView>
    {
        // Get all products
        //Task<IEnumerable<TProductView>> GetAllForViewAsync();
        // Get a single product
        //Task<TProductView> GetAsync(Guid? id);
        // Get products by sorting/filtering
    }
}
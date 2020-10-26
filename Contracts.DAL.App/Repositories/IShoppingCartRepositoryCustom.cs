using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IShoppingCartRepositoryCustom : IShoppingCartRepositoryCustom<ShoppingCart>
    {
    }

    public interface IShoppingCartRepositoryCustom<TShoppingCart>
    {
        // For admin to see every shopping cart
        //Task<IEnumerable<TShoppingCart>> GetAllForViewAsync();
        // For admin to get a single shopping cart
        //Task<TShoppingCart> FindForUserAsync(Guid? id, Guid userId);
        // For client to get his/her shopping cart
        //Task<TShoppingCart> GetForUserAsync(Guid? id);
    }
}
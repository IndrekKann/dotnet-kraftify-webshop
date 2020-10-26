using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderRepositoryCustom : IOrderRepositoryCustom<Order>
    {
    }

    public interface IOrderRepositoryCustom<TOrder>
    {
        // Admin gets all the orders
        //Task<IEnumerable<TOrder>> GetAllAsync();
        // Admin gets specific order by id
        //Task<TOrder> GetSingleAsync(Guid id);
        // User gets all his orders
        //Task<IEnumerable<TOrder>> GetAllForUserAsync(Guid userId);
        // User gets the specific order belonging to him/her
        //Task<TOrder> GetForUserAsync(Guid id, Guid userId);
    }
}
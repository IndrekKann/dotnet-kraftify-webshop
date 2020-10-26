using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>, IProductRepositoryCustom
    {
        Task<IEnumerable<ProductView>> GetAllForViewAsync();
        Task<IEnumerable<Product>> GetSearchAsync(string search, object? userId = null, bool noTracking = true);
        Task<IEnumerable<Product>> GetSearchTypeAsync(string search, object? userId = null, bool noTracking = true);
        Task<ProductView> FirstOrDefaultViewAsync(Guid id, object? userId = null, bool noTracking = true);
    }
}
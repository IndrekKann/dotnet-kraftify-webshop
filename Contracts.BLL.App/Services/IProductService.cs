using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IProductService : IBaseEntityService<Product>, IProductRepositoryCustom<ProductView>
    {
        Task<IEnumerable<ProductView>> GetAllForViewAsync(string? search, string? searchType, string? order, string? limit, string? page);
        Task<IEnumerable<Product>> GetSearchAsync(string search);
        Task<IEnumerable<Product>> GetSearchTypeAsync(string search);
        Task<int> GetPagesAmount(string? limit);
        Task<IEnumerable<string>> GeneratePagination(int totalPages, int currentPage = 1);
        Task<IEnumerable<Product>> GetOrderedProducts(string order, IEnumerable<Product> products);
        Task<ProductView> FirstOrDefaultViewAsync(Guid id);
    }
    
}

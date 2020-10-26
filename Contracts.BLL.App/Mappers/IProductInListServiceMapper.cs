using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IProductInListServiceMapper : IBaseMapper<DALAppDTO.ProductInList, BLLAppDTO.ProductInList>
    {
        BLLAppDTO.ProductInListView MapProductInListView(DALAppDTO.ProductInListView inObject);
    }
}
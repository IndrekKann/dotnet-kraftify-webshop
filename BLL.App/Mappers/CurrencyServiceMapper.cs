using Contracts.BLL.App.Mappers;
using ee.itcollege.webshop.indrek.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class CurrencyServiceMapper : BaseMapper<DALAppDTO.Currency, BLLAppDTO.Currency>, ICurrencyServiceMapper
    {
        
    }
}
﻿using ee.itcollege.webshop.indrek.Contracts.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface ICurrencyServiceMapper : IBaseMapper<DALAppDTO.Currency, BLLAppDTO.Currency>
    {
        
    }
}
using AutoMapper;
using AutoMapper.Configuration;
using ee.itcollege.webshop.indrek.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProductTypeServiceMapper : BaseMapper<DALAppDTO.ProductType, BLLAppDTO.ProductType>, IProductTypeServiceMapper
    {
        public ProductTypeServiceMapper() : base()
        {
            
        }

    }
}
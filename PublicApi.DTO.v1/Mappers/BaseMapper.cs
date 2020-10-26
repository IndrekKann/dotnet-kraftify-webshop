using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public abstract class BaseMapper<TLeftObject, TRightObject> : ee.itcollege.webshop.indrek.DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}
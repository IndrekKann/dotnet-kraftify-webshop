using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using ee.itcollege.webshop.indrek.DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class LangStrTranslationRepository:
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.LangStrTranslation, DAL.App.DTO.LangStrTranslation>,
        ILangStrTranslationRepository
    {
        public LangStrTranslationRepository(AppDbContext repoDbContext) : base(repoDbContext, new DALMapper<Domain.App.LangStrTranslation, DTO.LangStrTranslation>())
        {
        }
    }
}
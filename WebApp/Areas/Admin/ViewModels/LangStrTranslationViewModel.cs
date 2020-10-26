using Domain.App;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class LangStrTranslationViewModel
    {
        public LangStrTranslation LanguageStringTranslation { get; set; }
        public SelectList? LanguageStringTranslationSelectList { get; set; }
    }
}
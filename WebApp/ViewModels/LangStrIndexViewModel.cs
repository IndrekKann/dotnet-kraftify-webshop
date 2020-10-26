#pragma warning disable 1591
using System;

namespace WebApp.ViewModels
{
    public class LangStrIndexViewModel
    {
        public Guid Id { get; set; }
        public string CurrentValue { get; set; } = default!;
        public int CultureCount { get; set; }
    }
}

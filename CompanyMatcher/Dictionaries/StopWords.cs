using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyMatcher.Dictionaries
{
    public static class StopWords
    {
        private static readonly string[] LegalForms =
        [
            "ооо", "зао", "пао", "оао", "ао", "нао",
            "нко", "кб", "акб", "филиал", "банк", "банка",
            "гк", "асв", "ку", "общество",
            "акционерное", "публичное", "с", "ограниченной",
            "ответственностью", "открытое", "закрытое"
        ];

        private static readonly string[] Regions =
        [
            "спб", "москва", "москвы", "московский",
            "амурский", "амурская", "красноярский", "красноярск",
            "рф", "россии", "российский", "филиал", "ф",
            "г", "район", "р-н", "района"
        ];

        private static readonly string[] Grammar =
        [
            "г", "в", "на", "по", "для", "из", "от", "до",
            "и", "или", "не", "л"
        ];

        public static readonly string[] AllStopWords =
            LegalForms.Concat(Regions).Concat(Grammar).ToArray();
    }
}

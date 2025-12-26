using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CompanyMatcher.Algorithms
{
    internal class AbbreviationMatcher
    {
        public static bool IsAbbreviationMatch(string str1, string str2)
        {
            // Очищаем не буквы и не цифры
            string clean1 = Regex.Replace(str1, @"[^\p{L}\d]", "");
            string clean2 = Regex.Replace(str2, @"[^\p{L}\d]", "");

            // Определяем, какое слово - аббревиатура
            string shorter = clean1.Length < clean2.Length ? clean1 : clean2;
            string longer = shorter == clean1 ? clean2 : clean1;

            // Собираем аббревиатуру из длинного слова
            string abbr = "";

            foreach (char c in longer)
            {
                if (char.IsUpper(c) || char.IsDigit(c))
                {
                    abbr += c.ToString().ToLower();
                }
            }

            // Снижаем регистр изначальной абрревиатуры
            string abbrInitial = shorter.ToLower();

            // Сравниваем строки
            bool result = abbrInitial == abbr;
            return result;
        }
    }
}

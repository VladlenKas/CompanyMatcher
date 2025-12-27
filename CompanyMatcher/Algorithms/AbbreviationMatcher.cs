using System.Text.RegularExpressions;

namespace CompanyMatcher.Algorithms;

internal class AbbreviationMatcher
{
    public static bool IsAbbreviationMatch(string str1, string str2)
    {
        // Очищаем не буквы и не цифры
        string clean1 = ExtractStrings(str1);
        string clean2 = ExtractStrings(str2);

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

    private static string ExtractStrings(string str)
    {
        return Regex.Replace(str, @"[^\p{L}\p{N}]", "");
    }
}

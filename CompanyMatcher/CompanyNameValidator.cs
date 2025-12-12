using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace CompanyMatcher;

internal class CompanyNameValidator
{
    private readonly string[] _stopWords =
        [
            "ооо", "зао", "пао", "оао", "ао", "нао",
            "нко", "кб", "акб", "филлиал", "банк", "банка",
            "гк", "асв", "ку", "г", "в", "на", "по", "для",
            "из", "от", "до", "и", "или", "не", "район",
            "р-н", "района", "ф", "л", "общество", 
            "акционерное", "публичное", "с", "ограниченной",
            "ответственностью", "открытое", "закрытое"
        ];

    public bool AreSameCompany(string name1, string name2)
    {
        // 1. роверка на аббревиатуру
        if (IsAbbreviation(name1, name2))
        {
            return true;
        }

        // Нормализация строк
        string nameNormalized1 = NormalizeString(name1);
        string nameNormalized2 = NormalizeString(name2);

        // 2. Проверка на подстроки
        if (nameNormalized1.Contains(nameNormalized2) ||
            nameNormalized2.Contains(nameNormalized1))
        {
            return true;
        }

        // 3. Проверка на числа (если есть, должны быть одинаковые)
        string number1 = Regex.Match(nameNormalized1, @"\d+").Value;
        string number2 = Regex.Match(nameNormalized2, @"\d+").Value;

        if (!string.IsNullOrEmpty(number1) &&
            !string.IsNullOrEmpty(number2))
        {
            if (number1 != number2)
            {
                return false;
            }
        }

        // 4. Проверка после сортировки слов в строке
        bool sortResult = AreSameAfterSorting(nameNormalized1, nameNormalized2);
        if (sortResult)
        {
            return true;
        }

        // 5. Проверка по расстоянию Левенштейна
        double similarity = LevenshteinCalculate.CalculateSimilarity(nameNormalized1, nameNormalized2);
        return similarity >= 0.7;
    }

    private string NormalizeString(string str)
    {
        string strNormalize = str;

        // Удаление спецсимволов и приведение к нижнему регистру
        strNormalize = Regex.Replace(strNormalize, @"[,.;:!?@#\$%\^&\*\(\)\[\]\{\}<>\-_/|+=~]", "");
        strNormalize = strNormalize.ToLower();

        // Удаляем стоп-слова из списка
        foreach (string word in _stopWords)
        {
            string pattern = $@"\b{Regex.Escape(word)}\b";
            strNormalize = Regex.Replace(strNormalize, pattern, "");
        }

        // Замена множественных пробелов на один
        strNormalize = Regex.Replace(strNormalize, @"\s+", " ").Trim();

        return strNormalize;
    }

    private bool IsAbbreviation(string str1, string str2)
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

    private bool AreSameAfterSorting(string str1, string str2)
    {
        string[] words1 = str1.Split(' ');
        string[] words2 = str2.Split(' ');

        Array.Sort(words1);
        Array.Sort(words2);

        string sortStr1 = string.Join(" ", words1);
        string sortStr2 = string.Join(" ", words2);

        bool result = sortStr1.Contains(sortStr2) ||
            sortStr2.Contains(sortStr1);

        return result;
    }
};

using CompanyMatcher.Algorithms;

namespace CompanyMatcher.Main;

internal class CompanyNameComparer
{
    public bool AreSameCompany(string name1, string name2)
    {
        // Проверка на пустые строки
        if (string.IsNullOrWhiteSpace(name1) || string.IsNullOrWhiteSpace(name2))
            return false;

        // Проверка на аббревиатуру
        if (AbbreviationMatcher.IsAbbreviationMatch(name1, name2))
            return true;

        // Нормализация строк
        CompanyNameNormalizer.Normalize(ref name1);
        CompanyNameNormalizer.Normalize(ref name2);

        // Проверка на подстроки
        if (name1.Contains(name2) || name2.Contains(name1))
            return true;

        // Проверка на числа (если есть, должны быть одинаковые)
        if (NumbersMatcher.IsNumbersMismatch(name1, name2))
            return false;

        // Проверка по расстоянию Левенштейна
        if (LevenshteinSimilarity.Match(name1, name2))
            return true;

        return false;
    }
};

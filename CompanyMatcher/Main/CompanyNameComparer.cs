using CompanyMatcher.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

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
        string name1Norm = CompanyNameNormalizer.Normalize(name1);
        string name2Norm = CompanyNameNormalizer.Normalize(name2);

        // Проверка на подстроки
        if (name1Norm.Contains(name2Norm) || name2Norm.Contains(name1Norm))
            return true;

        // Проверка на числа (если есть, должны быть одинаковые)
        if (NumbersMatcher.IsNumbersMismatch(name1Norm, name2Norm))
            return false;

        // Проверка после сортировки слов в строке
        if (SortingMatcher.IsMatchAfterSort(name1Norm, name2Norm))
            return true;

        // Проверка по расстоянию Левенштейна
        if (LevenshteinSimilarity.Match(name1Norm, name2Norm))
            return true;

        return false;
    }
};

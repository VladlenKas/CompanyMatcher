using CompanyMatcher.Dictionaries;
using System.Text.RegularExpressions;

namespace CompanyMatcher.Algorithms;

internal static class CompanyNameNormalizer
{
    public static void Normalize(ref string str)
    {
        str = string.Join(" ", str
            .ToLower()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => Regex.Replace(word, @"[^\p{L}\p{N}]", ""))  // Только буквы и цифры
            .OrderBy(word => word, StringComparer.Ordinal)
            .Where(word => !StopWords.AllStopWords.Contains(word)));
    }
}

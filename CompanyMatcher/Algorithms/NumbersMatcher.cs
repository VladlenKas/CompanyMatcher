using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompanyMatcher.Algorithms;

internal static class NumbersMatcher
{
    public static bool IsNumbersMismatch(string str1, string str2)
    {
        // Очищаем все кроме цифр
        string numbers1 = ExtractNumbers(str1);
        string numbers2 = ExtractNumbers(str2);

        // Проверяем, что обе строки не пустые и не противоположны
        // Иначе завершаем проверку, возвращая, что строки разные
        if ((!string.IsNullOrEmpty(numbers1) && 
            !string.IsNullOrEmpty(numbers2)) ||
            numbers1 != numbers2)
        {
            return true;
        }

        return false;
    }

    private static string ExtractNumbers(string str)
    {
        return Regex.Replace(str, @"\D", "");
    }
}

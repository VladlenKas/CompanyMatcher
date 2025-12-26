using CompanyMatcher.Dictionaries;
using System.Text.RegularExpressions;

namespace CompanyMatcher.Algorithms
{
    internal static class CompanyNameNormalizer
    {
        public static string Normalize(string str)
        {
            string strNormalize = str;

            // Удаление спецсимволов и приведение к нижнему регистру
            strNormalize = Regex.Replace(strNormalize, @"[,.;:!?@#\$%\^&\*\(\)\[\]\{\}<>\-_/|+=~]", "");
            strNormalize = strNormalize.ToLower();

            // Удаляем стоп-слова из списка
            foreach (string word in StopWords.AllStopWords)
            {
                string pattern = $@"\b{Regex.Escape(word)}\b";
                strNormalize = Regex.Replace(strNormalize, pattern, "");
            }

            // Замена множественных пробелов на один
            strNormalize = Regex.Replace(strNormalize, @"\s+", " ").Trim();

            return strNormalize;
        }
    }
}

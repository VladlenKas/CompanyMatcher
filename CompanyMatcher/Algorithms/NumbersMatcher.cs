using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CompanyMatcher.Algorithms
{
    internal static class NumbersMatcher
    {
        public static bool IsNumbersMismatch(string str1, string str2)
        {
            string number1 = Regex.Match(str1, @"\d+").Value;
            string number2 = Regex.Match(str2, @"\d+").Value;

            // Пропускаем проверку, если чисел нет
            if (string.IsNullOrEmpty(number1) ||
                string.IsNullOrEmpty(number2))
            {
                return false;
            }

            // Пропускаем проверку, если числа одинаковые
            // (чтобы выполнялись следующие проверки)
            if (number1 != number2) return true;
            else return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CompanyMatcher.Algorithms
{
    internal static class SortingMatcher
    {
        public static bool IsMatchAfterSort(string str1, string str2)
        {
            string[] words1 = str1.Split(' ');
            string[] words2 = str2.Split(' ');

            Array.Sort(words1);
            Array.Sort(words2);

            string sortStr1 = string.Join(" ", words1);
            string sortStr2 = string.Join(" ", words2);

            if (sortStr1.Contains(sortStr2) ||
                sortStr2.Contains(sortStr1))
            {
                return true;
            }

            return false;
        }
    }
}

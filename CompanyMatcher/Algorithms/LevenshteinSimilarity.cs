namespace CompanyMatcher.Algorithms;

internal class LevenshteinSimilarity
{
    // Возвращает результат проверки схожести слов между собой
    public static bool Match(string str1, string str2)
    {
        double similarity = CalculateSimilarity(str1, str2);
        return similarity >= 0.7; // Здесь задаем коэфицент схожести слов
    }

    // Вычисляет коэффициент похожести двух строк
    private static double CalculateSimilarity(string str1, string str2)
    {
        int distance = LevenshteinDistance(str1, str2);
        int maxLength = Math.Max(str1.Length, str2.Length);

        return 1.0 - (double)distance / maxLength;
    }

    // Вычисляет расстояние Левенштейна между двумя строками
    private static int LevenshteinDistance(string str1, string str2)
    {
        var n = str1.Length + 1;
        var m = str2.Length + 1;
        var matrixD = new int[n, m];

        const int deletionCost = 1;
        const int insertionCost = 1;

        for (var i = 0; i < n; i++)
        {
            matrixD[i, 0] = i;
        }

        for (var j = 0; j < m; j++)
        {
            matrixD[0, j] = j;
        }

        for (var i = 1; i < n; i++)
        {
            for (var j = 1; j < m; j++)
            {
                var substitutionCost = str1[i - 1] == str2[j - 1] ? 0 : 1;

                matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
                                        matrixD[i, j - 1] + insertionCost,         // вставка
                                        matrixD[i - 1, j - 1] + substitutionCost); // замена
            }
        }

        return matrixD[n - 1, m - 1];
    }

    // Находит минимальное из трех чисел
    private static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;
}

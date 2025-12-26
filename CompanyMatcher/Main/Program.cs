using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyMatcher.Main;

// Модель для маппинга данных из json
internal class TestCase
{
    [JsonProperty("str1")] public string Str1 { get; set; } = null!;
    [JsonProperty("str2")] public string Str2 { get; set; } = null!;
    [JsonProperty("expected")] public bool Expected { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var testCases = ReadJson(@"test_cases.json");
            if (testCases == null)
            {
                Console.WriteLine("Ошибка: не удалось загрузить тестовые данные");
                return;
            }

            // Запускам проверку тестов
            var validator = new CompanyNameComparer();
            RunTests(validator, testCases);

            Console.WriteLine($"\nТесты завершены");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Ошибка: файл test_cases.json не найден");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Ошибка при разборе JSON: {ex.Message}");
        }
    }

    private static void RunTests(CompanyNameComparer validator, List<TestCase> testCases)
    {
        int passedCount = 0;
        int failedCount = 0;

        for (int i = 0; i < testCases.Count; i++)
        {
            var test = testCases[i];
            bool result = validator.AreSameCompany(test.Str1, test.Str2);
            bool isPassed = test.Expected == result;

            string status = isPassed ? "PASSED" : "FAILED";
            ConsoleColor color = isPassed ? ConsoleColor.Green : ConsoleColor.Red;

            Console.ForegroundColor = color;
            Console.WriteLine($"TEST #{i + 1:00} [{status}]:");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"> {test.Str1} <-> {test.Str2}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"> (Ожидается: {test.Expected} / Получено: {result})\n");
            Console.ResetColor();

            if (isPassed) passedCount++;
            else failedCount++;
        }

        // Процент успешности
        double successRate = testCases.Count == 0
            ? 0
            : (double)passedCount / testCases.Count * 100;

        // Итоговая статистика
        Console.ForegroundColor = passedCount == testCases.Count ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine($"Результат: {passedCount}/{testCases.Count} тестов пройдено");
        Console.WriteLine($"Процент успешности: {successRate:F2}%");
        Console.ResetColor();

        if (failedCount > 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Провалено: {failedCount} тестов");
            Console.ResetColor();
        }
    }


    private static List<TestCase>? ReadJson(string jsonFilePath)
    {
        string jsonString = File.ReadAllText(jsonFilePath);
        var testCases = JsonConvert.DeserializeObject<List<TestCase>>(jsonString);
        return testCases;
    }
}

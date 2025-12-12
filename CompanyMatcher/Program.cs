using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyMatcher;

// Модель для маппинга данных из json в виде списка тестов
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

            // Создаем и запускам валидатор
            var validator = new CompanyNameValidator();
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

    private static void RunTests(CompanyNameValidator validator, List<TestCase> testCases)
    {
        int passedCount = 0;
        int failedCount = 0;

        for (int i = 0; i < testCases.Count; i++)
        {
            var test = testCases[i];
            bool result = validator.AreSameCompany(test.Str1, test.Str2);

            if (test.Expected != result)
            {
                Console.WriteLine($"TEST №{i} FAILED: {test.Str1} <-> {test.Str2}");
                failedCount++;
            }
            else
            {
                passedCount++;
            }
        }

        Console.WriteLine($"\nПройдено: {passedCount}/{testCases.Count}");
        Console.WriteLine($"Провалено: {failedCount}/{testCases.Count}");
    }
    
    private static List<TestCase>? ReadJson(string jsonFilePath)
    {
        string jsonString = File.ReadAllText(jsonFilePath);
        var testCases = JsonConvert.DeserializeObject<List<TestCase>>(jsonString);
        return testCases;
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace IJunior { 
class Program
{
    #region Consts
    const int StartingPoints = 25;

    const int OverheadLimit = 10;

    const char HasPointSymbol = '#';
    const char DoesNotHavePointSymbol = '_';
    #endregion

    #region Console Output
    private static void PrintStats(Dictionary<string, int> stats)
    {
        foreach (var pair in stats)
            PrintStat(pair.Key.ToUpper(), pair.Value);
    }
    private static void PrintStat(string statName, int statValue)
    {
        string statVisual =
            new string(HasPointSymbol, statValue)
            .PadRight(OverheadLimit, DoesNotHavePointSymbol);
        PrintFormatted(statName, statVisual);
    }
    private static void PrintAge(int value)
    {
        PrintFormatted("Возраст", value.ToString());
    }
    private static void PrintPoints(int value)
    {
        PrintFormatted("Поинтов", value.ToString());
    }
    private static void PrintFormatted(string name, string value)
    {
        Console.WriteLine("{0} - {1}", name, value);
    }
    #endregion

    #region Console Input
    private static int ReadInt(string message)
    {
        Console.WriteLine(message);
        return ReadInt();
    }
    private static int ReadInt()
    {
        int result;
        while (!int.TryParse(Console.ReadLine(), out result)) { }
        return result;
    }
    private static string ReadOperation(string message)
    {
        Console.WriteLine(message);
        return ReadOperation();
    }
    private static string[] validOperations = new[] { "+", "-" };
    private static string ReadOperation()
    {
        string result;
        do 
        result = Console.ReadLine();
        while (!validOperations.Contains(result));
        return result;
    }
    private static string ReadLineToLower(string message)
    {
        Console.WriteLine(message);
        return ReadLineToLower();
    }
    private static string ReadLineToLower()
    {
        return Console.ReadLine().ToLower();
    }
    #endregion

    #region Stat Buying
    private static int BuyStat(Dictionary<string, int> stats, string statName, int currentPoints, int pointsInvested)
    {
        pointsInvested -= CalculateOverhead(pointsInvested, stats[statName]);
        stats[statName] += pointsInvested;
        return currentPoints - pointsInvested;
    }
    private static int CalculateOverhead(int pointsInvested, int currentStatValue)
    {
        int overhead = pointsInvested - (OverheadLimit - currentStatValue);
        return overhead < 0 ? 0 : overhead;
    }
    #endregion

    #region Gameplay
    private static void Intro()
    {
        Console.WriteLine("Добро пожаловать в меню выбора создания персонажа!");
        Console.WriteLine("У вас есть 25 очков, которые вы можете распределить по умениям");
        Console.WriteLine("Нажмите любую клавишу чтобы продолжить...");
        Console.ReadKey();
    }
    private static void StartPointAssignmentLoop(int points, Dictionary<string, int> stats)
    {
        while (points > 0)
        {
            Console.Clear();
            PrintPoints(points);
            PrintStats(stats);

            string statToChange = ReadLineToLower("Какую характеристику вы хотите изменить?");
            string operation = ReadOperation(@"Что вы хотите сделать? +\-");
            int operandPoints = ReadInt("Количество поинтов которое следует " + (operation == "+" ? "прибавить" : "отнять"));

            if (operation == "+")
                points = BuyStat(stats, statToChange, points, operandPoints);
            else if (operation == "-")
                points = BuyStat(stats, statToChange, points, -operandPoints);
            else
                throw new InvalidOperationException();
        }
    }
    private static int GetAge()
    {
        return ReadInt("Вы распределили все очки. Введите возраст персонажа:");
    }
    private static void ShowResultingCharacter(Dictionary<string, int> stats, int age)
    {
        Console.Clear();
        PrintAge(age);
        PrintStats(stats);
    }
    #endregion

    public static void Main(string[] args)
    {
        int age;
        int points = StartingPoints;
        Dictionary<string, int> stats = new Dictionary<string, int>
        {
            { "сила", 0 },
            { "ловкость", 0 },
            { "интеллект", 0 },
        };

        Intro();
        StartPointAssignmentLoop(points, stats);
        age = GetAge();
        ShowResultingCharacter(stats, age);
    }
} }
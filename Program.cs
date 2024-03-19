using Microsoft.Data.Sqlite;

namespace HabitTrackerApp;

class Program
{
    static void Main(string[] args)
    {
        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                @"CREATE TABLE IF NOT EXISTS Habits
                (id INTEGER PRIMARY KEY AUTOINCREMENT,
                 title TEXT NOT NULL,
                 goal INTEGER NOT NULL,
                 current INTEGER NOT NULL)";
            command.ExecuteNonQuery();
            DisplayMenu();
        }
    }
    private static void DisplayMenu()
    {
       bool isRunning = true;
       while (isRunning)
       {
           Console.WriteLine("Welcome to the Habit Tracker App");
           Console.WriteLine("1. Add a new habit");
           Console.WriteLine("2. View all habits");
           Console.WriteLine("3. Update a habit");
           Console.WriteLine("4. Delete a habit");
           Console.WriteLine("0. Exit");
           Console.Write("Please enter a number to select an option: ");
           string? choice = Console.ReadLine();
           switch (choice)
           {
               case "1":
                   AddHabit();
                   break;
               case "2":
                   ViewAllHabits();
                   break;
               case "3":
                   UpdateHabit();
                   break;
               case "4":
                   DeleteHabit();
                   break;
               case "0":
                   Console.WriteLine("Goodbye!");
                   isRunning = false;
                   break;
               default:
                   Console.WriteLine("Invalid choice. Please try again.");
                   break;
           }
       }
    }
    private static void GoalExplanation()
    {
        Console.WriteLine("The goal is the number of times you want to perform the habit in a day.");
        Console.WriteLine("The current is the number of times you have performed the habit today.");
    }
    private static void AddHabit()
    {
        var title = AskUser(  out var goal, out var current);
        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText =
                @"INSERT INTO Habits (title, goal, current)
                VALUES ($title, $goal, $current)";
            command.Parameters.AddWithValue("$title", title);
            command.Parameters.AddWithValue("$goal", goal);
            command.Parameters.AddWithValue("$current", current);
            command.ExecuteNonQuery();
        }
    }

    private static void ValidateIntInput(out int input)
    {
        bool isParse = int.TryParse(Console.ReadLine(), out input);
        while (!isParse)
        {
            Console.Write("Invalid input. Please enter a number: ");
            isParse = int.TryParse(Console.ReadLine(), out input);
        }
    }
    private static string? AskUser(out int goal, out int current)
    {
        Console.Write("Enter the title of the habit: ");
        string? title = Console.ReadLine();
        GoalExplanation();
        Console.Write("Enter the goal for the habit: ");
        ValidateIntInput(out goal);
        Console.Write("Enter the current progress for the habit: ");
        ValidateIntInput(out current);
        return title;
    }

    private static void ViewAllHabits()
    {
        throw new NotImplementedException();
    }
    private static void UpdateHabit()
    {
        throw new NotImplementedException();
    }
    private static void DeleteHabit()
    {
        throw new NotImplementedException();
    }
}
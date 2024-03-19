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
                 current INTEGER NOT NULL,
                 target INTEGER NOT NULL)";
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
    private static void AddHabit()
    {
        throw new NotImplementedException();
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
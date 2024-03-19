using ConsoleTables;
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
        Console.WriteLine("The goal is the number of times you want to perform the habit in a month.");
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
        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Habits";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var title = reader.GetString(1);
                    var goal = reader.GetInt32(2);
                    var current = reader.GetInt32(3);
                    CreateTable("#", "Title", "Goal", "Current", id, title, goal, current); 
                    Console.WriteLine();
                }
            }
        }
    }
    private static void CreateTable(string row1, string row2, string row3,string row4, int child1, string child2, int child3, int child4)
    {
        var table = new ConsoleTable(row1, row2, row3,row4);
        table.AddRow(child1, child2, child3, child4);
        table.Write();
    }
    private static void UpdateHabit()
    {
        Console.Write("Which habit would you like to update? Enter the ID: "); 
        ValidateIntInput(out int id);
        ValidateMaxId(id);
        Console.Write("Enter the new current progress for the habit: ");
        ValidateIntInput(out int current);
        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Habits SET current = $current WHERE id = $id";
            command.Parameters.AddWithValue("$current", current);
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }
    }
    private static void ValidateMaxId(int id)
    {
        int maxId;
        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT MAX(id) FROM Habits";
            maxId = Convert.ToInt32(command.ExecuteScalar());
        }
        while (id > maxId)
        {
            Console.Write("ID does not exist. Please enter a valid ID: ");
            ValidateIntInput(out id);
        }
    }
    private static void DeleteHabit()
    {
        Console.Write("Which habit would you like to delete? Enter the ID: ");
        ValidateIntInput(out int id);
        ValidateMaxId(id);
        using (var connection = new SqliteConnection("Data Source=HabitTracker.db"))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Habits WHERE id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }
    }
}
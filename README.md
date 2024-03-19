# Habit Tracker App

This is a simple console-based Habit Tracker application that allows users to add, view, update, and delete habits. It utilizes SQLite for data storage.

## Installation

1. Clone or download the repository.
2. Ensure you have .NET Core SDK installed on your machine.
3. Navigate to the project directory.
4. Run the following command to build the application:
```  
dotnet build
```

## Usage

1. Run the application using the following command:
```
dotnet run
```
2. You will be presented with a menu to perform various actions.
3. Choose an option by entering the corresponding number:
   - 1: Add a new habit
   - 2: View all habits
   - 3: Update a habit
   - 4: Delete a habit
   - 0: Exit

## Dependencies

- Microsoft.Data.Sqlite
- TableConsoles

## Database

The application utilizes SQLite as its database backend. Upon running the application, a database file named `HabitTracker.db` will be created in the same directory.
The database schema includes a table `Habits` with the following columns:
- id (INTEGER, PRIMARY KEY, AUTOINCREMENT)
- title (TEXT, NOT NULL)
- goal (INTEGER, NOT NULL)
- current (INTEGER, NOT NULL)
- target (INTEGER, NOT NULL)

## Contributing


Pull requests are welcome. 

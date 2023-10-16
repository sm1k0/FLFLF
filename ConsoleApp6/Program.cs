using System;
using System.Collections.Generic;

class Program
{
    static List<Note> notes = new List<Note>();
    static int currentDateIndex = 0;
    static int currentTaskIndex = 0;

    static void Main(string[] args)
    {
        InitializeNotes();

        Console.WriteLine("Ежедневник");
        Console.WriteLine("------------------");

        while (true)
        {
            DisplayMenu();
            HandleInput();
        }
    }

    static void InitializeNotes()
    {
        notes.Add(new Note("Заметка 1", "Описание заметки 1", new DateTime(2022, 6, 6)));
        notes.Add(new Note("Заметка 2", "Описание заметки 2", new DateTime(2022, 6, 7)));
        notes.Add(new Note("Заметка 3", "Описание заметки 3", new DateTime(2022, 6, 8)));
        notes.Add(new Note("Заметка 4", "Описание заметки 4", new DateTime(2022, 6, 9)));
        notes.Add(new Note("Заметка 5", "Описание заметки 5", new DateTime(2022, 6, 10)));
    }

    static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("Ежедневник");
        Console.WriteLine("------------------");

        DateTime currentDate = notes[currentDateIndex].Date;
        Console.WriteLine($"Дата: {currentDate.ToShortDateString()}");
        Console.WriteLine();

        for (int i = 0; i < notes.Count; i++)
        {
            Note note = notes[i];
            string noteTitle = (i == currentTaskIndex) ? $"=> {note.Title}" : note.Title;
            Console.WriteLine($"{i + 1}. {noteTitle}");
        }
    }

    static void HandleInput()
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.UpArrow)
        {
            currentTaskIndex = Math.Max(0, currentTaskIndex - 1);
        }
        else if (keyInfo.Key == ConsoleKey.DownArrow)
        {
            currentTaskIndex = Math.Min(notes.Count - 1, currentTaskIndex + 1);
        }
        else if (keyInfo.Key == ConsoleKey.Enter)
        {
            DisplayFullNote();
        }
        else if (keyInfo.Key == ConsoleKey.LeftArrow)
        {
            ChangeDate(-1);
        }
        else if (keyInfo.Key == ConsoleKey.RightArrow)
        {
            ChangeDate(1);
        }
    }

    static void DisplayFullNote()
    {
        Console.Clear();
        Console.WriteLine("Полная информация о заметке");
        Console.WriteLine("------------------");

        Note note = notes[currentDateIndex];
        Console.WriteLine($"Дата: {note.Date.ToShortDateString()}");
        Console.WriteLine($"Заметка: {note.Title}");
        Console.WriteLine($"Описание: {note.Description}");
        Console.WriteLine("------------------");

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    static void ChangeDate(int increment)
    {
        DateTime currentDate = notes[currentDateIndex].Date;
        int newIndex = -1;

        for (int i = 0; i < notes.Count; i++)
        {
            if (notes[i].Date == currentDate)
            {
                newIndex = i;
                break;
            }
        }

        if (newIndex != -1)
        {
            newIndex += increment;

            if (newIndex < 0)
            {
                newIndex = notes.Count - 1;
            }
            else if (newIndex >= notes.Count)
            {
                newIndex = 0;
            }

            currentDateIndex = newIndex;
            currentTaskIndex = 0;
        }
    }
}

class Note
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public Note(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
    }
}
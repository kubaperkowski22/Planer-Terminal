﻿using Planer;
using System.Text;

bool IsProgramRunning = true;
List<Plan> PlanList = new List<Plan>();
string VisibleText = "";

while (IsProgramRunning)
{
    StartMainMenu();
    (int left, int top) = Console.GetCursorPosition();
    var option = 1;
    var decorator = "✅ \u001b[32m";
    ConsoleKeyInfo key;
    bool isSelected = false;


    while (!isSelected)
    {
        Console.SetCursorPosition(left, top);

        ShowMainOptions(option, decorator);

        key = Console.ReadKey(false);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (option == 1) option = 4;
                else option -= 1;
                break;

            case ConsoleKey.DownArrow:
                if (option == 4) option = 1;
                else option += 1;
                break;

            case ConsoleKey.Enter:
                isSelected = true;
                break;
        }
    }

    switch (option)
    {
        case 1:
            AddNewPlan();
            break;
        case 2:
            EditPlan();
            break;
        case 3:
            BrowsePlans();
            break;
        case 4:
            IsProgramRunning = false;
            break;
    }

}

void StartMainMenu()
{
    Console.Clear();
    Console.OutputEncoding = Encoding.UTF8;
    Console.CursorVisible = false;
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Witaj w Planerze!");
    Console.ResetColor();
    Console.WriteLine("\nUżywaj klawisza ⬆️ oraz ⬇️ aby poruszać się po menu. Kliknij \u001b[32mEnter\u001b[0m aby wybrać.");
}
void ShowMainOptions(int option, string decorator)
{
    Console.WriteLine($"{(option == 1 ? decorator : "   ")}Add new plan\u001b[0m");
    Console.WriteLine($"{(option == 2 ? decorator : "   ")}Edit plans\u001b[0m");
    Console.WriteLine($"{(option == 3 ? decorator : "   ")}Browse your plans\u001b[0m");
    Console.WriteLine($"{(option == 4 ? decorator : "   ")}Exit\u001b[0m");
}
void AddNewPlan()
{
    Console.Clear();

    VisibleText += "Tworzysz nowy plan\n\n";
    Console.WriteLine("Tworzysz nowy plan\n");

    Plan plan = CreateNewPlan();
    PlanList.Add(plan);
}
Plan CreateNewPlan()
{
    VisibleText += "Podaj nazwę planu: \n";
    Console.WriteLine("Podaj nazwę planu: ");

    string name = Console.ReadLine();
    VisibleText += name + '\n';
    Console.WriteLine("Podaj datę końca wydarzenia(RRRR-MM-DD): ");
    VisibleText += "Podaj datę końca wydarzenia(RRRR-MM-DD): \n";
    DateTime dateTime;

    bool isDateTimeFormatCorrect = false;
    while (!isDateTimeFormatCorrect)
    {
        string dateTimeString = Console.ReadLine();
        if (!CheckDateTimeFormat(dateTimeString))
        {
            Console.Clear();
            Console.WriteLine(VisibleText);
            continue;
        }
        dateTime = DateTime.Parse(dateTimeString);

        if (dateTime < DateTime.Now)
        {
            Console.Clear();
            Console.WriteLine("BŁĄD! Data wydarzenia nie może być wcześniejsza niż aktualna data!");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            Console.WriteLine(VisibleText);
        }
        else
        {
            VisibleText += dateTime;
            VisibleText += '\n';
            isDateTimeFormatCorrect = true;
        }
    }

    Console.WriteLine("Podaj priorytet (1-10): ");
    VisibleText += "Podaj priorytet (1-10): \n";
    int priority = Convert.ToInt32(Console.ReadLine());

    bool isPriorityCorrect = false;
    while (!isPriorityCorrect)
    {
        if (CheckPriority(priority))
        {
            VisibleText += priority + '\n';
            isPriorityCorrect = true;
        }
        else
        {
            Console.WriteLine(VisibleText);
            priority = Convert.ToInt32(Console.ReadLine());
        }
    }
    
    Console.WriteLine("Podaj kategorię: ");
    string category = ChooseCategory();
    System.Threading.Thread.Sleep(1000);

    Console.WriteLine("Pomyslnie stworzono nowy plan.\n\n");
    Console.WriteLine(".d8888b 888  888 .d8888b .d8888b .d88b. .d8888b .d8888b  \r\n88K     888  888d88P\"   d88P\"   d8P  Y8b88K     88K      \r\n\"Y8888b.888  888888     888     88888888\"Y8888b.\"Y8888b. \r\n     X88Y88b 888Y88b.   Y88b.   Y8b.         X88     X88 \r\n 88888P' \"Y88888 \"Y8888P \"Y8888P \"Y8888  88888P' 88888P' ");

    System.Threading.Thread.Sleep(3000);
    Console.Clear();

    return new Plan(name, priority, category);
}
bool CheckDateTimeFormat(string dateTime)
{
    if (string.IsNullOrEmpty(dateTime) || dateTime.Length < 9)
    {
        Console.Clear();
        Console.WriteLine("BŁĄD! Nie podałeś daty!");
        System.Threading.Thread.Sleep(3000);
        Console.Clear();

        return false;
    }
    if (dateTime[4] != ' ' && dateTime[4] != '-' && dateTime[4] != '/' && dateTime[4] != '.')
    {
        Console.Clear();
        Console.WriteLine("BŁĄD! Niepoprawny format!");
        System.Threading.Thread.Sleep(3000);
        Console.Clear();

        return false;
    }
    if (dateTime[7] != ' ' && dateTime[7] != '-' && dateTime[7] != '/' && dateTime[7] != '.')
    {
        Console.Clear();
        Console.WriteLine("BŁĄD! Niepoprawny format");
        System.Threading.Thread.Sleep(3000);
        Console.Clear();

        return false;
    }
    else return true;
}
DateTime PromptDateTime()
{
    var day = Convert.ToInt32(Console.ReadLine());
    var month = Convert.ToInt32(Console.ReadLine());
    var year = Convert.ToInt32(Console.ReadLine());

    return new DateTime(year, month, day, 0, 0, 0, 0);
}
bool CheckPriority(int priority)
{
    if (priority < 0 || priority > 10)
    {
        Console.Clear();
        Console.WriteLine("BŁĄD! Podany priorytet wykracza poza skale!");
        System.Threading.Thread.Sleep(3000);
        Console.Clear();

        return false;
    }
    else return true;
}
void EditPlan()
{
    Console.Clear();
    Console.WriteLine("Wybierz plan, który chcesz edytować.\n");

    int i = 1;
    foreach(var plan in PlanList)
    {

    }

    Console.ReadKey();
}
void BrowsePlans()
{
    Console.Clear();
    Console.WriteLine("Przeglądasz swoje plany: ");

    foreach(Plan plan in PlanList)
    {
        Console.WriteLine(plan.EventName);
    }

    Console.WriteLine("\nWcisnij dowolny klawisz aby kontynuować.");
    Console.ReadKey();
}
string ChooseCategory()
{
    string[] category = { "Sport", "Relax", "Work", "Food", "Automotive", "Health", "Games", "Study" };

    (int left, int top) = Console.GetCursorPosition();
    var option = 1;
    var decorator = "✅ \u001b[32m";
    ConsoleKeyInfo key;
    bool isSelected = false;


    while (!isSelected)
    {
        Console.SetCursorPosition(left, top);

        Console.WriteLine($"{(option == 1 ? decorator : "   ")}Sport\u001b[0m");
        Console.WriteLine($"{(option == 2 ? decorator : "   ")}Relax\u001b[0m");
        Console.WriteLine($"{(option == 3 ? decorator : "   ")}Work\u001b[0m");
        Console.WriteLine($"{(option == 4 ? decorator : "   ")}Food\u001b[0m");
        Console.WriteLine($"{(option == 5 ? decorator : "   ")}Automotive\u001b[0m");
        Console.WriteLine($"{(option == 6 ? decorator : "   ")}Health\u001b[0m");
        Console.WriteLine($"{(option == 7 ? decorator : "   ")}Games\u001b[0m");
        Console.WriteLine($"{(option == 8 ? decorator : "   ")}Study\u001b[0m");

        key = Console.ReadKey(false);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (option == 1) option = 8;
                else option -= 1;
                break;

            case ConsoleKey.DownArrow:
                if (option == 8) option = 1;
                else option += 1;
                break;

            case ConsoleKey.Enter:
                isSelected = true;
                break;
        }
    }

    Console.Clear();

    return category[option-1];
}
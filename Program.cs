using Planer;
using System.Text;

bool IsProgramRunning = true;
List<Plan> PlanList = new List<Plan>();

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
    Console.WriteLine("Tworzysz nowy plan\n");
    Plan plan = CreateNewPlan();
    PlanList.Add(plan);
    Console.ReadKey();
}
Plan CreateNewPlan()
{
    Console.WriteLine("Podaj nazwę planu: ");
    string name = Console.ReadLine();
    Console.WriteLine("Podaj datę końca wydarzenia(RRRR-MM-DD): ");
    string dateTime = Console.ReadLine();
    Console.WriteLine("Podaj priorytet (1-10): ");
    int priority = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Podaj kategorię: ");
    string category = ChooseCategory();

    Console.WriteLine("Wybrałeś kategorię:" + category);

    //Console.WriteLine("Pomyslnie stworzono nowy plan.");
    return new Plan(name, priority, category);
}
void EditPlan()
{
    Console.Clear();
    Console.WriteLine("Edytujesz plan");
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
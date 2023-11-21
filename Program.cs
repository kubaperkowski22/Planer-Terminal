using Planer;
using System.ComponentModel.Design;
using System.Text;

bool IsProgramRunning = true;
List<Plan> PlanList = new List<Plan>();
string VisibleText = "";
string VisibleTextInEdit = "";

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
    Console.WriteLine($"{(option == 1 ? decorator : "   ")}Dodaj nowy plan\u001b[0m");
    Console.WriteLine($"{(option == 2 ? decorator : "   ")}Edytuj istniejący plan\u001b[0m");
    Console.WriteLine($"{(option == 3 ? decorator : "   ")}Przeglądaj istniejące plany\u001b[0m");
    Console.WriteLine($"{(option == 4 ? decorator : "   ")}Zakończ\u001b[0m");
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
    DateTime dateTime = DateTime.Now;

    bool isDateTimeFormatCorrect = false;
    while (!isDateTimeFormatCorrect)
    {
        string dateTimeString = Console.ReadLine();
        if (!CheckDateTimeFormat(dateTimeString, out dateTime))
        {
            Console.Clear();
            Console.Write(VisibleText);
            continue;
        }

        if (dateTime < DateTime.Now)
        {
            Console.Clear();
            Console.WriteLine("BŁĄD! Data wydarzenia nie może być wcześniejsza niż aktualna data!");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            Console.Write(VisibleText);
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
            Console.Clear();
            Console.Write(VisibleText);
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

    return new Plan(name, dateTime, priority, category);
}
bool CheckDateTimeFormat(string dateTime,out DateTime dateTime2)
{   
    if(!DateTime.TryParse(dateTime, out dateTime2))
    {
        Console.Clear();
        Console.WriteLine("BŁĄD! Błędna data!");
        System.Threading.Thread.Sleep(3000);
        Console.Clear();

        return false;
    }
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
bool CheckPriority(int priority)
{
    if (priority < 1 || priority > 10)
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
    Console.WriteLine("Wybierz plan, który chcesz edytować.");

    (int left, int top) = Console.GetCursorPosition();
    var option = 0;
    var decorator = "✅ \u001b[32m";
    ConsoleKeyInfo key;
    bool isSelected = false;


    while (!isSelected)
    {
        Console.SetCursorPosition(left, top);

        ShowAllPlans(option, decorator);

        key = Console.ReadKey(false);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (option == 0) option = PlanList.Count;
                else option -= 1;
                break;

            case ConsoleKey.DownArrow:
                if (option == PlanList.Count) option = 0;
                else option += 1;
                break;

            case ConsoleKey.Enter:
                isSelected = true;
                break;
        }
    }

    if(option != PlanList.Count)
    {
        ChangeDataInPlan(PlanList[option]);
    }
}
void ChangeDataInPlan(Plan plan)
{
    Console.Clear();
    Console.WriteLine("Edycja planu. Kliknij klawisz \"Enter\" aby pominąć daną informację.\n");

    Console.WriteLine($"Podaj nazwę planu (dawniej: {plan.EventName}):");
    string name = Console.ReadLine();
    if(!string.IsNullOrEmpty(name))
        plan.EventName = name;
    
    VisibleTextInEdit += $"Edycja planu. Kliknij klawisz \"Enter\" aby pominąć daną informację.\n\nPodaj nazwę planu (dawniej: {plan.EventName}):\n" + name + "\nPodaj datę końca wydarzenia(RRRR-MM-DD): \n";
    Console.WriteLine("Podaj datę końca wydarzenia(RRRR-MM-DD): ");
    DateTime dateTime = DateTime.Now;

    bool isDateTimeFormatCorrect = false;
    while (!isDateTimeFormatCorrect)
    {
        string dateTimeString = Console.ReadLine();
        if(string.IsNullOrEmpty(dateTimeString))
        {
            isDateTimeFormatCorrect = true;
            continue;
        }

        if (!CheckDateTimeFormat(dateTimeString, out dateTime))
        {
            Console.Clear();
            Console.Write(VisibleTextInEdit);
            continue;
        }

        if(!DateTime.TryParse(dateTimeString, out dateTime))
        {
            
        }

        if (dateTime < DateTime.Now)
        {
            Console.Clear();
            Console.WriteLine("BŁĄD! Data wydarzenia nie może być wcześniejsza niż aktualna data!");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            Console.Write(VisibleTextInEdit);
        }
        else
        {
            VisibleTextInEdit += dateTime;
            VisibleTextInEdit += '\n';
            isDateTimeFormatCorrect = true;
        }
        plan.Date = dateTime;
    }

    Console.WriteLine($"Podaj priorytet (1-10) (dawniej: {plan.Priority}):");
    VisibleTextInEdit += $"Podaj priorytet (1-10) (dawniej: {plan.Priority}): \n";

    bool isPriorityCorrect = false;
    while (!isPriorityCorrect)
    {
        string priorityString = Convert.ToString(Console.ReadLine());
        int priority;
        if (!string.IsNullOrEmpty(priorityString))
        {
            priority = Convert.ToInt32(priorityString);


            if (CheckPriority(priority))
            {
                VisibleTextInEdit += priority + '\n';
                isPriorityCorrect = true;
            }
            else
            {
                Console.Clear();
                Console.Write(VisibleTextInEdit);
                continue;
            }
            plan.Priority = priority;
        }
        else break;
    }
    

    Console.WriteLine("Podaj kategorię: ");
    string category = ChooseCategoryInEdit();
    if(category != "")
        plan.Category = category;
    VisibleText += "Podaj kategorię:\n" + plan.Category;
    Console.Write(VisibleTextInEdit);

    Console.WriteLine("Czy ukończono: ");
    (int left, int top) = Console.GetCursorPosition();
    var option = 1;
    var decorator = "✅ \u001b[32m";
    ConsoleKeyInfo key;
    bool isSelected = false;

    while (!isSelected)
    {
        Console.SetCursorPosition(left, top);

        Console.WriteLine($"{(option == 1 ? decorator : "   ")}TAK\u001b[0m");
        Console.WriteLine($"{(option == 2 ? decorator : "   ")}NIE\u001b[0m");

        key = Console.ReadKey(false);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (option == 1) option++;
                else option--;
                break;

            case ConsoleKey.DownArrow:
                if (option == 2) option--;
                else option++;
                break;

            case ConsoleKey.Enter:
                isSelected = true;
                break;
        }
        if (option == 1) plan.IsEventFinished = true;
        else plan.IsEventFinished = false;
    }

    Console.Clear();
    System.Threading.Thread.Sleep(1000);

    Console.WriteLine($"Pomyślnie zedytowano plan o nazwie: {plan.EventName}\n\n");
    Console.WriteLine(".d8888b 888  888 .d8888b .d8888b .d88b. .d8888b .d8888b  \r\n88K     888  888d88P\"   d88P\"   d8P  Y8b88K     88K      \r\n\"Y8888b.888  888888     888     88888888\"Y8888b.\"Y8888b. \r\n     X88Y88b 888Y88b.   Y88b.   Y8b.         X88     X88 \r\n 88888P' \"Y88888 \"Y8888P \"Y8888P \"Y8888  88888P' 88888P' ");

    System.Threading.Thread.Sleep(3000);
    Console.Clear();
    System.Threading.Thread.Sleep(1000);

}
void ShowAllPlans(int option, string decorator)
{
    for(int i = 0; i <= PlanList.Count; ++i)
    {
        if(i == PlanList.Count)
            Console.WriteLine($"{(option == i ? decorator : "   ")}Anuluj\u001b[0m");
        else
            Console.WriteLine($"{(option == i ? decorator : "   ")}{PlanList[i].EventName} {(PlanList[i].IsEventFinished ? "UKOŃCZONO":"")}\u001b[0m");
    }
}
void BrowsePlans()
{
    Console.Clear();
    Console.WriteLine("Wybierz plan, który chcesz przejrzeć.");

    (int left, int top) = Console.GetCursorPosition();
    var option = 0;
    var decorator = "✅ \u001b[32m";
    ConsoleKeyInfo key;
    bool isSelected = false;


    while (!isSelected)
    {
        Console.SetCursorPosition(left, top);

        ShowAllPlans(option, decorator);

        key = Console.ReadKey(false);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (option == 0) option = PlanList.Count;
                else option -= 1;
                break;

            case ConsoleKey.DownArrow:
                if (option == PlanList.Count) option = 0;
                else option += 1;
                break;

            case ConsoleKey.Enter:
                isSelected = true;
                break;
        }
    }

    if (option != PlanList.Count)
    {
        Console.Clear();
        Console.WriteLine($"Nazwa: {PlanList[option].EventName}");
        Console.WriteLine($"Data zakończenia: {PlanList[option].Date.Date}");
        Console.WriteLine($"Priorytet: {PlanList[option].Priority}");
        Console.WriteLine($"Kategoria: {PlanList[option].Category}");
        Console.WriteLine($"Czy ukończono: {(PlanList[option].IsEventFinished ? "TAK" : "NIE")}");

        Console.WriteLine("\nKliknij dowolny klawisz aby skończyć przeglądanie.");
        Console.ReadKey();
        Console.Clear();
    }
}

string ChooseCategory()
{
    string[] category = { "Sport", "Odpoczynek", "Praca", "Jedzenie", "Motoryzacja", "Zdrowie", "Gry", "Nauka" };

    (int left, int top) = Console.GetCursorPosition();
    var option = 1;
    var decorator = "✅ \u001b[32m";
    ConsoleKeyInfo key;
    bool isSelected = false;


    while (!isSelected)
    {
        Console.SetCursorPosition(left, top);

        Console.WriteLine($"{(option == 1 ? decorator : "   ")}Sport\u001b[0m");
        Console.WriteLine($"{(option == 2 ? decorator : "   ")}Odpoczynek\u001b[0m");
        Console.WriteLine($"{(option == 3 ? decorator : "   ")}Praca\u001b[0m");
        Console.WriteLine($"{(option == 4 ? decorator : "   ")}Jedzenie\u001b[0m");
        Console.WriteLine($"{(option == 5 ? decorator : "   ")}Motoryzacja\u001b[0m");
        Console.WriteLine($"{(option == 6 ? decorator : "   ")}Zdrowie\u001b[0m");
        Console.WriteLine($"{(option == 7 ? decorator : "   ")}Gry\u001b[0m");
        Console.WriteLine($"{(option == 8 ? decorator : "   ")}Nauka\u001b[0m");

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

string ChooseCategoryInEdit()
{
    string[] category = { "Sport", "Odpoczynek", "Praca", "Jedzenie", "Motoryzacja", "Zdrowie", "Gry", "Nauka" };

    (int left, int top) = Console.GetCursorPosition();
    var option = 1;
    var decorator = "✅ \u001b[32m";
    ConsoleKeyInfo key;
    bool isSelected = false;


    while (!isSelected)
    {
        Console.SetCursorPosition(left, top);

        Console.WriteLine($"{(option == 1 ? decorator : "   ")}Sport\u001b[0m");
        Console.WriteLine($"{(option == 2 ? decorator : "   ")}Odpoczynek\u001b[0m");
        Console.WriteLine($"{(option == 3 ? decorator : "   ")}Praca\u001b[0m");
        Console.WriteLine($"{(option == 4 ? decorator : "   ")}Jedzenie\u001b[0m");
        Console.WriteLine($"{(option == 5 ? decorator : "   ")}Motoryzacja\u001b[0m");
        Console.WriteLine($"{(option == 6 ? decorator : "   ")}Zdrowie\u001b[0m");
        Console.WriteLine($"{(option == 7 ? decorator : "   ")}Gry\u001b[0m");
        Console.WriteLine($"{(option == 8 ? decorator : "   ")}Nauka\u001b[0m");
        Console.WriteLine($"{(option == 9 ? decorator : "   ")}Pomiń\u001b[0m");

        key = Console.ReadKey(false);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (option == 1) option = 9;
                else option -= 1;
                break;

            case ConsoleKey.DownArrow:
                if (option == 9) option = 1;
                else option += 1;
                break;

            case ConsoleKey.Enter:
                isSelected = true;
                break;
        }
    }

    Console.Clear();

    if (option == 9) return "";
    else return category[option - 1];
}
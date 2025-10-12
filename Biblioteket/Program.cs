using System;

namespace Biblioteket
{
    internal class Program
    {
        static string[,] books =
        {
            //en 2D array (som tabell) som används för att räkna ut hut många tillgängliga böcker som finns av varje sort

            //kolumner:
            //[0] = titel
            //[1] = total (antal exemplar)
            //[2] = borrowed (antal utlånade böcker)
            // avalible = total - borrowed
            {"Tills alla dör", "4", "0" },
            {"Harry potter och de vises sten", "4", "0" },
            {"Sagan om de två tornen", "4", "0" },
            {"Pippi Långstrump", "4", "0" },
            {"Hungergames Catching fire", "4", "0" }
        };

        // i main anropas metoden RunProgram
        static void Main(string[] args)
        {
            RunProgram();
        }

        // denna metoden anropar alla andra andra metoder
        static void RunProgram()
        {
            GreetUser();
            string[][] users = UserSystem();

            const int MaxLoansPerUser = 5;
            int[][] userLoans = InitializeUserLoans(users.Length, MaxLoansPerUser);

            // loop för inloggningen, lyckad inloggning = menyn öppnas, efter tre misslyckade inloggning avslutas programmet
            while (true)
            {
                int userIndex = logIn(users);
                if (userIndex == -1)
                {
                    Console.WriteLine("För många misslyckade inloggningsförsök. Programmet avslutas.");
                    return;
                }

                // öppna meny för inloggad användare efter logout återgår man till inloggningen
                mainMenu(userIndex, users, userLoans);
            }
        }

        // välkomstmeddelande
        static void GreetUser()
        {
            Console.WriteLine("Välkommen till lånecentralen");
        }

        // metoden tar emot användare (Users) och ger tillbaka en int (static int)
        static int logIn(string[][] Users)
        {
            Console.Clear();
            int Attempts = 0;

            //gör så att man inte kan göra fler än 3 försök, då avbryts programmet
            while (Attempts < 3)
            {
                string UserName;
                
                //kontrollerar att användaren inte skriver siffror som användarnamn
                while (true)
                {
                    Console.Write("Användarnamn:");
                    UserName = Console.ReadLine();

                    //IsNullOrWhiteSpace kontrollerar så att användar inte skriver in tomt användarnamn
                    if (string.IsNullOrWhiteSpace(UserName))
                    {
                        Console.WriteLine("Användarnamnet får inte vara tomt");
                        continue;
                    }

                    // int.TryParse kontrollerar att användaren inte skriver siffror som användarnamn
                    if (int.TryParse(UserName, out _))
                    {
                        Console.WriteLine("Användarnamnet får inte innehålla siffror.");
                    }
                    else
                    {
                        break;
                    }
                }

                //en int som inehåller det giltiga PIN numret
                int UserPIN;               

                //en loop som använder TryParse för att kolla så att användaren faktiskt skriver in siffror som PIN, annars felmeddelande
                while (true)
                {
                    Console.Write("PIN:");
                    string inputPIN = Console.ReadLine();

                    if (int.TryParse(inputPIN, out UserPIN))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("PIN-koden ska bara innehålla siffror. Försök igen");
                    }
                }

               // jämför med Users listan
               for (int i = 0; i < Users.Length; i++)
                {
                    // "StringComparison.OrdinalIgnoreCase" jämför strings (användarens inmatning och Users namnet) utan att bry sig om stora/små bokstäver
                    if (Users[i][0].Equals(UserName, StringComparison.OrdinalIgnoreCase) && Users[i][1] == UserPIN.ToString())
                    {
                        Console.WriteLine($"\nVälkommen {Users[i][0]}! Du är inloggad");
                        return i;//om det matchar med Users listan returneras användarens index
                    }
                }

                // lägger till ett försök för varje fel inloggning
                Attempts++;
                Console.WriteLine($"Fel användarnamn eller PIN-kod, du har gjort {Attempts} försök");
            }

            // om användaren misslyckas returneras -1 som används av RunProgram för att avsluta programmet
            return -1;
            
        }

        // en metod som sparar användare i arrayer
        static string[][] UserSystem()
        {
            string[] UserOne = new string[] { "Anna", "1234" };
            string[] UserTwo = new string[] { "Funghione", "1235" };
            string[] UserThree = new string[] { "Tangione", "1236" };
            string[] UserFour = new string[] {"Oscar", "1237"};
            string[] UserFive = new string[] { "TregNeko", "1238" };
            // en array av arrayer [0] är användarnamnet och [1] är PIN
            string[][] Users = new string[][] { UserOne, UserTwo, UserThree, UserFour, UserFive };
            return Users;
        }

        // metod med jagged array som kollar vilka böcker användaren lånat. tar emot int userCount (hur många användare som finns) och slotsPerUser (hur många böcker man kan låna. returnerar int[][] (jagged array)
        static int[][] InitializeUserLoans(int userCount, int slotsPerUser)
        {
            //skapar en tom lista för varje användare
            int[][] loans = new int[userCount][];

            // gör en lista med 5 platser
            for (int i = 0; i < userCount; i++)
            {
                loans[i] = new int[slotsPerUser];
                for (int j = 0; j < slotsPerUser; j++)
                    loans[i][j] = -1; // -1 betyder tom plats eftersom index börjar på 0
            }
            return loans;
        }


        //metod som tar emot int och string men returnerar inget (static void)
        static void mainMenu(int userIndex, string[][] users, int[][] userLoans)
        {

            bool isRunning = true;//en bool som kör loopen så länge den är true

            while (isRunning)
            {
                Console.Clear();//så att konsollen rensas när menyn ska köras
                Console.WriteLine($"Inloggad som {users[userIndex][0]}\n");
                Console.WriteLine("1. Visa böcker");
                Console.WriteLine("2. Låna bok");
                Console.WriteLine("3. Lämna tillbaka bok");
                Console.WriteLine("4. Mina lån");
                Console.WriteLine("5. Logga ut");
                Console.Write("\nVälj en funktion genom att skriva in tillhörande siffra:");

                // varje siffra från val menyn anropar rätt metod 
                string input = Console.ReadLine();
                switch (input)//hanterar valen som finns i menyn
                {
                    case "1":
                        showBooks();
                        break;
                    case "2":
                        loanBook(userIndex, userLoans);
                        break;
                    case "3":
                        returnBook(userIndex, userLoans);
                        break;
                    case "4":
                        myLoans(userIndex, userLoans);
                        break;
                    case "5":
                        Console.WriteLine("Du har loggat ut.");
                        Console.WriteLine("Tryck enter för att återgå till inloggningen");
                        Console.ReadLine();
                        isRunning = false; //avslutar loopen och återgår till huvudmenyn
                        break;                       ;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen");
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nTryck enter för att återgå till menyn");
                    Console.ReadLine();
                }
            }            
        }

        // visar alla böcker 
        static void showBooks()
        {
            Console.Clear();
            Console.WriteLine("Tillgängliga böcker");

            //loopar igenom alla böcker i arrayen
            for (int i = 0; i < books.GetLength(0); i++)
            {
                //konverterar siffrorna som text till int och räknar ut hur många tillgängliga böcker som finns
                int total = int.Parse(books[i, 1]);
                int borrowed = int.Parse(books[i, 2]);
                int avalible = total - borrowed;

                //skriver ut info om böckernas tillgänglighet
                Console.WriteLine($"{i + 1}. {books[i, 0]}");
                Console.WriteLine($"Exemplar: {total}\nUtlånade: {borrowed}\nTillgängliga: {avalible}\n");
            }

            Console.WriteLine("Tryck enter för att återgå till menyn");            
        }

        static void loanBook(int userIndex, int[][] userLoans)
        {
            Console.Clear();
            Console.WriteLine("Låna bok"); 
            //anropar metoden showbooks när användaren vill låna bok
            showBooks();
            Console.WriteLine("Ange numret på boken du vill låna");

            string input = Console.ReadLine();

            //kontrollerar att användaren inte skriver in bokstäver eller en siffra som inte är mellan 1-5
            if (!int.TryParse(input, out int choice) || choice < 1 || choice > books.GetLength(0))
            {
                Console.WriteLine("Ogiltigt val");
                return;
            }

            //räknar ut tillgängliga böcker
            int bookIndex = choice - 1;
            int total = int.Parse(books[bookIndex, 1]);
            int borrowed = int.Parse(books[bookIndex, 2]);
            int avalible = total - borrowed;

            //säger att det finns inga böcker kvar om avalible är 0 eller mindre
            if (avalible <= 0)
            {
                Console.WriteLine($"Det finns inga tillgängliga exemplar av \"{books[bookIndex, 0]}\"");
                return;
            }

            //kollar om användaren har plats att låna fler böcker
            int avalibleSpot = -1;
            for (int i = 0; i < userLoans[userIndex].Length; i ++)
            {
                if (userLoans[userIndex][i] == - 1)
                {
                    avalibleSpot = i;
                    break;
                }
            }

            //om det inte finns lediga platser att låna bok säger den stopp
            if (avalibleSpot == - 1)
            {
                Console.WriteLine("Du kan inte låna fler böcker");
                return;
            }

            //boken lånas. bokens index läggs in i användarens lånelista
            userLoans[userIndex][avalibleSpot] = bookIndex;
            //antalet utlånade böcker ökas
            books[bookIndex, 2] = (borrowed + 1).ToString();
            Console.WriteLine($"Du har lånat \"{books[bookIndex, 0]}\"");
        }

        //nästan samma som loanBook fast tvärt om
        static void returnBook(int userIndex, int[][] userLoans)
        {
            Console.Clear();
            Console.WriteLine("Lämna tillbaka bok");

            //hämtar användarens lånalista
            int[] loans = userLoans[userIndex];
            bool hasLoans = false;

            //kollar om det finns några lån
            for (int i = 0; i < loans.Length; i++)
            {
                //om det finns lånade böcker blir hasLoans true
                if (loans[i] != - 1)
                {
                    hasLoans = true;
                    break;
                }
            }

            //om det inte finns lånade böcker och hasLoans är false säger den det till användaren
            if (!hasLoans)
            {
                Console.WriteLine("Du har inga lån nu");
                return;
            }

            Console.WriteLine("Dina lånade böcker");
            int displayIndex = 1;
            //loopar igenom använarens lån och visar titlar
            for (int i = 0; i < loans.Length; i++)
            {
                //skriver ut titlar på lånade böcker med hjälp av bokens index
                if (loans[i] != -1)
                {
                    Console.WriteLine($"{displayIndex}. {books[loans[i], 0]}");
                    displayIndex++;
                }
            }

            Console.Write("Ange numret på boken du vill lämna tillbaka");
            string input = Console.ReadLine();
            //läser in vilket nummer användaren anger
            if (!int.TryParse(input, out int returnChoice) || returnChoice < 1)
            {
                Console.WriteLine("Ogiltigt val");
                return;
            }

            //räknar igenom användarens lista och hittar rätt lån beroende på vad användren skrev in. när den hittar rätt sparas bokens index i position
            int position = -1;
            int counter = 0;
            for (int i = 0; i < loans.Length; i++)
            {
                if (loans[i] != - 1)
                {
                    counter++;
                    if (counter == returnChoice)
                    {
                        position = i;
                        break;
                    }                      
                }
            }

            //om användaren skriver in ogiltigt nummer blir position -1 och man får felmeddelande
            if (position == - 1)
            {
                Console.WriteLine("Ogiltigt val");
                return;
            }

            //lämnar tillbaka vald bok.
            //sparar vald bok med bookIndex
            int bookIndex = loans[position];
            //gör den låneplatsen legig igen
            loans[position] = -1;
            //minskar antalet utlånade böcker i books
            int borrowed = int.Parse(books[bookIndex, 2]);
            //gör så att antalet inte blir negativt med hjälp av Math.Max....
            books[bookIndex, 2] = Math.Max(0, borrowed - 1).ToString();

            Console.WriteLine($"Du har lämnat tillbaka \"{books[bookIndex, 0]}\"");
        }

        //metod för att se sina lån
        static void myLoans(int userIndex, int[][] userLoans)
        {
            Console.Clear();
            Console.WriteLine("Mina lån");

            //hämtar användarens lån från userLoans med userIndex
            int[] loans = userLoans[userIndex];
            bool hasLoans = false;
            //räknare som inte börjar på 0
            int display = 1;

            //går igenom användarens låneplatser
            for (int i = 0; i < loans.Length; i++)
            {
                //om användaren har några lån så visas det och hasLoans blir true
                if (loans[i] != - 1)
                {                   
                    Console.WriteLine($"{display}. {books[loans[i], 0]}");
                    hasLoans = true;
                    display++;
                }
            }

            //om inga lån finns så säger den det
            if (!hasLoans)
            {
                Console.WriteLine("Du har inga lån");
            }
        }
    }
}


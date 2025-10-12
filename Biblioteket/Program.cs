using System;

namespace Biblioteket
{
    internal class Program
    {
        static string[,] books =
        {
            //kolumner:
            //[0] = titel
            //[1] = antal exemplar
            //[2] = antal utlånade böcker
            {"Tills alla dör", "3", "1" },
            {"Harry potter och de vises sten", "3", "1" },
            {"Sagan om de två tornen", "3", "1" },
            {"Pippi Långstrump", "3", "1" },
            {"Hungergames Catching fire", "3", "1" }
        };
        static void Main(string[] args)
        {
            RunProgram();
        }

        static void RunProgram()
        {
            GreetUser();
            string[][] users = UserSystem();

            const int MaxLoansPerUser = 5;
            int[][] userLoans = InitializeUserLoans(users.Length, MaxLoansPerUser);

            while (true)
            {
                int userIndex = logIn(users);
                if (userIndex == -1)
                {
                    Console.WriteLine("För många misslyckade inloggningsförsök. Programmet avslutas.");
                    return;
                }

                // Öppna meny för inloggad användare; efter logout återgår vi till inloggningen
                mainMenu(userIndex, users, userLoans);
            }
        }

        static void GreetUser()
        {
            Console.WriteLine("Välkommen till lånecentralen");
        }

        static int logIn(string[][] Users)
        {
            Console.Clear();
            int Attempts = 0;

            //säger att man får inte göra fler än 3 försök, då avbryts programmet
            while (Attempts < 3)
            {
                string UserName;
                
                //kontrollerar att användaren inte skriver siffror som användarnamn
                while (true)
                {
                    Console.Write("Användarnamn:");
                    UserName = Console.ReadLine();

                    //kontrollerar så att användar inte skriver in tomt användarnamn
                    if (string.IsNullOrWhiteSpace(UserName))
                    {
                        Console.WriteLine("Användarnamnet får inte vara tomt");
                        continue;
                    }

                    //kontrollerar att användaren inte skriver siffror som användarnamn
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

               //söker igenom alla användare och returnerar ett index 
               for (int i = 0; i < Users.Length; i++)
                {
                    if (Users[i][0].Equals(UserName, StringComparison.OrdinalIgnoreCase) && Users[i][1] == UserPIN.ToString())
                    {
                        Console.WriteLine($"\nVälkommen {Users[i][0]}! Du är inloggad");
                        return i;//användarens index om man är inloggad
                    }
                }

                // lägger till ett försök för varje fel inloggning
                Attempts++;
                Console.WriteLine($"Fel användarnamn eller PIN-kod, du har gjort {Attempts} försök");
            }

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

            string[][] Users = new string[][] { UserOne, UserTwo, UserThree, UserFour, UserFive };
            return Users;
        }

        static int[][] InitializeUserLoans(int userCount, int slotsPerUser)
        {
            int[][] loans = new int[userCount][];
            for (int i = 0; i < userCount; i++)
            {
                loans[i] = new int[slotsPerUser];
                for (int j = 0; j < slotsPerUser; j++)
                    loans[i][j] = -1; // -1 betyder tom plats
            }
            return loans;
        }


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

        static void showBooks()
        {
            Console.Clear();
            Console.WriteLine("Tillgängliga böcker");

            //loopar igenom alla böcker i arrayen
            for (int i = 0; i < books.GetLength(0); i++)
            {
                //konverterar siffrorna som text tilll int
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
            showBooks();
            Console.WriteLine("Ange numret på boken du vill låna");

            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > books.GetLength(0))
            {
                Console.WriteLine("Ogiltigt val");
                return;
            }

            int bookIndex = choice - 1;
            int total = int.Parse(books[bookIndex, 1]);
            int borrowed = int.Parse(books[bookIndex, 2]);
            int avalible = total - borrowed;

            if (avalible <= 0)
            {
                Console.WriteLine($"Det finns inga tillgängliga exemplar av \"{books[bookIndex, 0]}\"");
                return;
            }

            int avalibleSpot = -1;
            for (int i = 0; i < userLoans[userIndex].Length; i ++)
            {
                if (userLoans[userIndex][i] == - 1)
                {
                    avalibleSpot = i;
                    break;
                }
            }

            if (avalibleSpot == - 1)
            {
                Console.WriteLine("Du kan inte låna fler böcker");
                return;
            }

            userLoans[userIndex][avalibleSpot] = bookIndex;
            books[bookIndex, 2] = (borrowed + 1).ToString();
            Console.WriteLine($"Du har lånat \"{books[bookIndex, 0]}\"");
        }

        static void returnBook(int userIndex, int[][] userLoans)
        {
            Console.Clear();
            Console.WriteLine("Lämna tillbaka bok");

            int[] loans = userLoans[userIndex];
            bool hasLoans = false;

            for (int i = 0; i < loans.Length; i++)
            {
                if (loans[i] != - 1)
                {
                    hasLoans = true;
                    break;
                }
            }

            if (!hasLoans)
            {
                Console.WriteLine("Du har inga lån nu");
                return;
            }

            Console.WriteLine("Dina lånade böcker");
            int displayIndex = 1;
            for (int i = 0; i < loans.Length; i++)
            {
                if (loans[i] != -1)
                {
                    Console.WriteLine($"{displayIndex}. {books[loans[i], 0]}");
                    displayIndex++;
                }
            }

            Console.Write("Ange numret på boken du vill lämna tillbaka");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int returnChoice) || returnChoice < 1)
            {
                Console.WriteLine("Ogiltigt val");
                return;
            }

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

            if (position == - 1)
            {
                Console.WriteLine("Ogiltigt val");
                return;
            }

            int bookIndex = loans[position];
            loans[position] = -1;
            int borrowed = int.Parse(books[bookIndex, 2]);
            books[bookIndex, 2] = Math.Max(0, borrowed - 1).ToString();

            Console.WriteLine($"Du har lämnat tillbaka \"{books[bookIndex, 0]}\"");
        }

        static void myLoans(int userIndex, int[][] userLoans)
        {
            Console.Clear();
            Console.WriteLine("Mina lån");

            int[] loans = userLoans[userIndex];
            bool hasLoans = false;
            int display = 1;
            for (int i = 0; i < loans.Length; i++)
            {
                if (loans[i] != - 1)
                {                   
                    Console.WriteLine($"{display}. {books[loans[i], 0]}");
                    hasLoans = true;
                    display++;
                }
            }

            if (!hasLoans)
            {
                Console.WriteLine("Du har inga lån");
            }
        }
    }
}
//GÖR EN NY BRANCH!!!!!!!

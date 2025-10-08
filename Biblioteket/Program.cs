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
            GreetUser();
            string [][] users = UserSystem();
            logIn(users);            
            mainMenu(true);
            showBooks();
        }

        static void GreetUser()
        {
            Console.WriteLine("Välkommen till lånecentralen");
        }

        static void logIn(string[][] Users)
        {
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
                bool validPIN = false;

                //en loop som använder TryParse för att kolla så att användaren faktiskt skriver in siffror som PIN, annars felmeddelande
                while (true)
                {
                    Console.Write("PIN:");
                    string inputPIN = Console.ReadLine();

                    if (int.TryParse(inputPIN, out UserPIN))
                    {
                        validPIN = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("PIN-koden ska bara innehålla siffror. Försök igen");
                    }
                }

                //en loop som kollar användarnamn och PIN 
                bool logInSuccess = false;
                foreach (string[] user in Users)
                {
                    string name = user[0];
                    string PIN = user[1];

                    //if sats som kollar om användaren skriver in rätt och konverterar UserPIN till en string så det ska funka
                    if (UserName == name && UserPIN.ToString() == PIN)
                    {
                        Console.WriteLine($"\nVälkommen {UserName}! Du är inloggad.");
                        logInSuccess = true;
                        mainMenu(logInSuccess);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Fel användarnamn eller PIN-kod. Försök igen");
                        break;
                    }
                }

                // räknar hur många försök man gjort
                if (!logInSuccess)
                {
                    Attempts++;
                    Console.WriteLine($"Du har gjort {Attempts} försök.");
                }
                else if (Attempts == 3)
                {
                    Console.WriteLine("För många försök! Programmet avslutas.");
                }
                else if (logInSuccess)
                {
                    break;
                }

            }
            
        }

        // en metod som sparar användare i arrayer
        static string[][] UserSystem()
        {
            string[] UserOne = ["Anna", "1234"];
            string[] UserTwo = ["Funghione", "1235"];
            string[] UserThree = ["Tangione", "1236"];
            string[] UserFour = ["Oscar", "1237"];
            string[] UserFive = ["TregNeko", "1238"];
            string[][] Users = [UserOne, UserTwo, UserThree, UserFour, UserFive];
            return Users;
        }

        static void mainMenu(bool logInSuccess)
        {
     
            if (!logInSuccess)
            {
                return;//om inloggning misslyckas hoppa över menyn
            }

            bool isRunning = true;//en bool som kör loopen så länge den är true

            while (isRunning)
            {
                Console.Clear();//så att konsollen rensas när menyn ska köras
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
                        loanBooks();
                        break;
                    case "3":
                        returnBook();
                        break;
                    case "4":
                        myLoans();
                        break;
                    case "5":
                        Console.WriteLine("Du har loggat ut.");
                        Console.WriteLine("Tryck enter för att återgå till inloggningen");
                        Console.ReadLine();
                        isRunning = false; //avslutar loopen och återgår till huvudmenyn
                        break;

                        break;
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
            Console.ReadLine();
        }
    }
}


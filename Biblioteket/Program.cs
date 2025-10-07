namespace Biblioteket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GreetUser();
            string [][] users = UserSystem();
            StartMenu(users);
        }

        static void GreetUser()
        {
            Console.WriteLine("Välkommen till lånecentralen");
        }

        static void StartMenu(string[][] Users)
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

            }
            Console.WriteLine("För många försök! Programmet avslutas.");
        }

        // en metod som sparar användare i arrayer
        static string[][] UserSystem()
        {
            string[] UserOne = ["Anna", "1234"];
            string[] UserTwo = ["Cajsa", "1235"];
            string[] UserThree = ["Kitang", "1236"];
            string[] UserFour = ["Oscar", "1237"];
            string[] UserFive = ["TregNeko", "1238"];
            string[][] Users = [UserOne, UserTwo, UserThree, UserFour, UserFive];
            return Users;
        }
    }
}

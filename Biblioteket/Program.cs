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
            Console.Write("Användarnamn:");
            string UserName = Console.ReadLine();
            
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

            bool logInSuccess = false;

            //en loop som kör igenom alla användare
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
            }
            // om användaren skriver fel får man ett felmeddelande och kan föröka igen 
            if (!logInSuccess)
            {
                Console.WriteLine("\nFel användarnamn elle PIN. Försök igen");
                StartMenu(Users);
            }
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

namespace Biblioteket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartMenu();
            UserSystem();
        }

        static void StartMenu()
        {
            Console.WriteLine("Välkommen till lånecentralen");
            Console.Write("Användarnamn:");
            Console.ReadLine();
            Console.Write("PIN:");
            Console.ReadLine();
        }

        static void UserSystem()
        {
            string[] UserOne = ["Anna", "1234"];
            string[] UserTwo = ["Cajsa", "1235"];
            string[] UserThree = ["Kitang", "1236"];
            string[] UserFour = ["Oscar", "1237"];
            string[] UserFive = ["TregNeko", "1238"];
            string[][] Users = [UserOne, UserTwo, UserThree, UserFour, UserFive];
        }
    }
}

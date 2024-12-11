using MarysCandyShop;

namespace MarysCandyShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataSeed.SeedData();
            UserInterface.RunMainMenu();

        }
    }
}

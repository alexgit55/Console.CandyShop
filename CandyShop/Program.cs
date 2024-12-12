using MarysCandyShop;

namespace MarysCandyShop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataSeed.SeedData();

            var productsController = new ProductsController();
            productsController.CreateDatabase();

            UserInterface.RunMainMenu();

        }
    }
}

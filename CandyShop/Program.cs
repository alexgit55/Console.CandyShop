using MarysCandyShop;

namespace MarysCandyShop
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var productsController = new ProductsController();
            productsController.CreateDatabase();

            UserInterface.RunMainMenu();

        }
    }
}

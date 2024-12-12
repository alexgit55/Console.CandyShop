using MarysCandyShop.Models;
using Spectre.Console;

namespace MarysCandyShop
{
    internal static class UserInterface
    {
        internal const string divider = "------------------------------";

        internal static void RunMainMenu()
        {
            var isMenuRunning = true;
            string menuMessage = "Press any key to go back to the Menu";
            var productsController = new ProductsController();

            while (isMenuRunning)
            {
                PrintHeader();
                var userChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<Enums.MainMenuOptions>()
                    .Title("What would you like to do?")
                    .AddChoices(
                        Enums.MainMenuOptions.ViewProducts, 
                        Enums.MainMenuOptions.ViewSingleProduct,
                        Enums.MainMenuOptions.AddProduct, 
                        Enums.MainMenuOptions.DeleteProduct, 
                        Enums.MainMenuOptions.UpdateProduct,
                        Enums.MainMenuOptions.Exit)

                );

                Console.WriteLine(divider);

                switch (userChoice)
                {
                    case Enums.MainMenuOptions.ViewProducts:
                        var products = productsController.GetProducts();
                        ViewProducts(products);
                        break;
                    case Enums.MainMenuOptions.ViewSingleProduct:
                        var productChoice = GetProductChoice();
                        ViewProduct(productChoice);
                        break;
                    case Enums.MainMenuOptions.AddProduct:
                        var product = GetProductInput();
                        productsController.AddProduct(product);
                        break;
                    case Enums.MainMenuOptions.DeleteProduct:
                        var productToDelete = GetProductChoice();
                        productsController.DeleteProduct(productToDelete);
                        break;
                    case Enums.MainMenuOptions.UpdateProduct:
                        var productToUpdate = GetProductChoice();
                        var updatedProduct = GetProductUpdateInput(productToUpdate);
                        productsController.UpdateProduct(updatedProduct);
                        break;
                    case Enums.MainMenuOptions.Exit:
                        isMenuRunning = false;
                        menuMessage = "Goodbye!\n";
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose one of the above");
                        break;
                }

                Console.WriteLine(divider);
                Console.WriteLine(menuMessage);
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static Product GetProductUpdateInput(Product productToUpdate)
        {
            Console.Clear();
            ViewProduct(productToUpdate);

            Console.WriteLine("You'll be prompted with the choice to updated each property. Press enter" +
                "for Yes and N for no.");

            productToUpdate.Name = AnsiConsole.Confirm("Update name?") ? AnsiConsole.Ask<string>("Product's new name: ") : productToUpdate.Name;

            productToUpdate.Price = AnsiConsole.Confirm("Update price?") ? AnsiConsole.Ask<decimal>("Product's new price: ") : productToUpdate.Price;

            var type = productToUpdate.Type;
            var updateType = AnsiConsole.Confirm("Update type?");
            

            if (updateType)
            {
                type = AnsiConsole.Prompt(
                    new SelectionPrompt<Enums.ProductType>()
                    .Title("What type of product is this?")
                    .AddChoices(Enums.ProductType.ChocolateBar, Enums.ProductType.Lollipop)
                );
            }
            if (type == Enums.ProductType.ChocolateBar)
            {
                Console.WriteLine("Cocoa percentage: ");
                var cocoaPercentage = int.Parse(Console.ReadLine());
                return new ChocolateBar(productToUpdate.Id)
                {
                    Name = productToUpdate.Name,
                    Price = productToUpdate.Price,
                    CocoaPercentage = cocoaPercentage
                };
            }
            else
            {
                Console.WriteLine("Shape: ");
                var shape = Console.ReadLine();
                return new Lollipop(productToUpdate.Id)
                {
                    Name = productToUpdate.Name,
                    Price = productToUpdate.Price,
                    Shape = shape
                };
            }
        }

        private static void ViewProduct(Product productChoice)
        {
            var panel = new Panel(productChoice.GetProductForPanel());
            panel.Header = new PanelHeader("Product details");
            panel.Padding(2, 2, 2, 2);

            AnsiConsole.Write(panel);

        }

        private static Product GetProductChoice()
        {
            var productsController = new ProductsController();
            var products = productsController.GetProducts();
            var productsArray = products.Select(products => products.Name).ToArray();
            var productChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Which product would you like to view?")
                .AddChoices(productsArray)
            );

            return products.Single(x => x.Name == productChoice);
        }

        [Obsolete]
        internal static void ViewProducts(List<Product> products)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Name");
            table.AddColumn("Type");
            table.AddColumn("Price");
            table.AddColumn("CocoaPercentage");
            table.AddColumn("Shape");

            foreach (var product in products)
            {
                table.AddRow(product.GetColumnsArray());
            }

            AnsiConsole.Write(table);
        }

        internal static void PrintHeader()
        {
            var title = "Mary's Candy Shop";

            var dateTime = DateTime.Now;
            var daysSinceOpening = Helpers.GetDaysSinceOpening();
            var todaysProfit = 5.5m;
            var targetAchieved = false;

            Console.WriteLine(@$"{title}
{divider}
Today's date: {dateTime}
Days since opening: {daysSinceOpening}
Today's profit: ${todaysProfit}
Today's target achieved: {targetAchieved}
{divider}
");
        }

        private static Product GetProductInput()
        {
            Console.WriteLine("Product name: ");
            var productName = Console.ReadLine();
            while (!Validation.IsStringValid(productName))
            {
                Console.WriteLine("Product name cannot be empty or have more than 20 characters. Please try again: ");
                productName = Console.ReadLine();
            }

            Console.WriteLine("Product price: ");
            var priceInput = Console.ReadLine();
            var priceValidation = Validation.IsPriceValid(priceInput);
            while (!priceValidation.IsValid)
            {
                Console.WriteLine(priceValidation.Message);
                priceInput= Console.ReadLine();
                priceValidation = Validation.IsPriceValid(priceInput);
            }

            var type = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.ProductType>()
                .Title("What type of product is this?")
                .AddChoices(Enums.ProductType.ChocolateBar, Enums.ProductType.Lollipop)
            );

            if (type == Enums.ProductType.ChocolateBar)
            {
                Console.WriteLine("Cocoa percentage: ");
                var cocoaInput = Console.ReadLine();
                var cocoaPercentageValidation = Validation.IsCocoaPercentageValid(cocoaInput);
                while (!cocoaPercentageValidation.IsValid)
                {
                    Console.WriteLine(cocoaPercentageValidation.Message);
                    cocoaInput = Console.ReadLine();
                    cocoaPercentageValidation = Validation.IsCocoaPercentageValid(cocoaInput);
                }

                return new ChocolateBar()
                {
                    Name = productName,
                    Price = priceValidation.Price,
                    CocoaPercentage = cocoaPercentageValidation.CocoaPercentage
                };
            }
            else
            {
                Console.WriteLine("Shape: ");
                var shapeInput = Console.ReadLine();
                while (!Validation.IsStringValid(shapeInput))
                {
                    Console.WriteLine("Shape cannot be empty or have more than 20 characters. Please try again: ");
                    shapeInput = Console.ReadLine();

                }
                return new Lollipop()
                {
                    Name = productName,
                    Price = priceValidation.Price,
                    Shape = shapeInput
                };
            }
        }
    }
}

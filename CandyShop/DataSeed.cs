using MarysCandyShop.Models;

namespace MarysCandyShop
{
    internal class DataSeed
    {
        string[] CandyNames = ["Snickers", "Mars", "Twix", "Bounty", "KitKat"];
  
        internal static void SeedData()
        {

            List<Product> products =
            [
                // Chocolate Bars
                new ChocolateBar(1) {Name = "Dark Delight", Price = 2.99m, CocoaPercentage = 70 },
                new ChocolateBar(2) {Name = "Milk Magic", Price = 2.49m, CocoaPercentage = 50 },
                new ChocolateBar(3) {Name = "White Wonder", Price = 2.79m, CocoaPercentage = 30 },
                new ChocolateBar(4) {Name = "Hazelnut Heaven", Price = 3.19m, CocoaPercentage = 65 },
                new ChocolateBar(5) {Name = "Orange Infusion", Price = 3.29m, CocoaPercentage = 55 },

                // Lollipops
                new Lollipop(6) {Name = "Cherry Twist", Price = 1.99m, Shape = "Round" },
                new Lollipop(7) {Name = "Lemon Swirl", Price = 1.89m, Shape = "Twisted" },
                new Lollipop(8) {Name = "Grape Galaxy", Price = 1.99m, Shape = "Star" },
                new Lollipop(9) {Name = "Strawberry Burst", Price = 1.95m, Shape = "Heart" },
                new Lollipop(10) {Name = "Blueberry Bliss", Price = 2.09m, Shape = "Oval" },
            ];

            var productsController = new ProductsController();

            productsController.AddProducts(products);
        }
    }
}

using MarysCandyShop.Models;
using Microsoft.Data.Sqlite;
using static MarysCandyShop.Enums;

namespace MarysCandyShop
{
    internal class ProductsController
    {
        private string ConnectionString { get; } = "Data Source=products.db";

        internal void CreateDatabase()
        {
            try
            {
                using (var connection = new SqliteConnection(ConnectionString))
                {
                    connection.Open();
                    using var command = connection.CreateCommand();
                    command.CommandText = @"CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Price DECIMAL NOT NULL,
                    CocoaPercentage INTEGER NULL,
                    Shape TEXT NULL,
                    Type INTEGER NOT NULL
                )";
                    command.ExecuteNonQuery();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        internal List<Product> GetProducts()
        {
            var products = new List<Product>();

            try
            {
                using (StreamReader reader = new StreamReader(Configuration.DocPath))
                {
                    reader.ReadLine(); // Skip headers
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] parts = line.Split(',');

                        if (int.Parse(parts[1]) == (int)ProductType.ChocolateBar)
                        {
                            var product = new ChocolateBar(int.Parse(parts[0]));
                            product.Name = parts[2];
                            product.Price = decimal.Parse(parts[3]);
                            product.CocoaPercentage = int.Parse(parts[4]);
                            products.Add(product);
                        }
                        else if (int.Parse(parts[1]) == (int)ProductType.Lollipop)
                        {
                            var product = new Lollipop(int.Parse(parts[0]));
                            product.Name = parts[2];
                            product.Price = decimal.Parse(parts[3]);
                            product.Shape = parts[5];
                            products.Add(product);
                        }


                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error loading products: {e.Message}");
                Console.WriteLine(UserInterface.divider);
            }

            return products;
        }


        internal void AddProduct(Product product)
        {

            var id = GetProducts().Count+1;

            try
            {
                using (StreamWriter writer = new StreamWriter(Configuration.DocPath, true))
                {
                    // Check if file is empty and add headers first
                    if (writer.BaseStream.Length <= 3)
                    {
                        writer.WriteLine("Id,Type,Name,Price,CocoaPercentage,Shape");
                    }
                    
                    var csvLine = product.GetProductForCSV(id);
                    writer.WriteLine(csvLine);
                
                }
                Console.WriteLine("Product saved to file");
            }
            catch (Exception e)
            {   
                Console.WriteLine($"There was an error saving product: {e.Message}");
                Console.WriteLine(UserInterface.divider);
            }
        }

        internal void AddProducts(List<Product> products)
        {

            try
            {
                using (StreamWriter writer = new StreamWriter(Configuration.DocPath))
                {
                    writer.WriteLine("Id,Type,Name,Price,CocoaPercentage,Shape");
                    foreach (var product in products)
                    {
                        var csvLine = product.GetProductForCSV(product.Id);
                        writer.WriteLine(csvLine);
                    }
                }
                Console.WriteLine("Products saved to file");
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error saving products: {e.Message}");
                Console.WriteLine(UserInterface.divider);
            }
        }
        internal void DeleteProduct(Product product)
        {
            var products = GetProducts();
            var updatedProducts = products.Where(x => x.Id != product.Id).ToList();
            AddProducts(updatedProducts);
        }

        internal void UpdateProduct(Product product)
        {
            var products = GetProducts();
            var updatedProducts = products.Where(x => x.Id != product.Id).ToList();
            updatedProducts.Add(product);

            AddProducts(updatedProducts);
        }
    }
}

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
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Products";

                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var type = (ProductType)reader.GetInt32(5);
                        var name = reader.GetString(1);
                        var price = reader.GetDecimal(2);
                        switch (type)
                        {
                            case ProductType.ChocolateBar:
                                var cocoaPercentage = reader.GetInt32(3);
                                products.Add(new ChocolateBar(id)
                                {
                                    Name = name,
                                    Price = price,
                                    CocoaPercentage = cocoaPercentage
                                });
                                break;
                            case ProductType.Lollipop:
                                var shape = reader.GetString(4);
                                products.Add(new Lollipop(id)
                                {
                                    Name = name,
                                    Price = price,
                                    Shape = shape
                                });
                                break;
                        }
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

            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = product.GetInsertQuery();
                product.AddParameters(command);
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {   
                Console.WriteLine($"There was an error saving product: {e.Message}");
                Console.WriteLine(UserInterface.divider);
            }
        }

        internal void DeleteProduct(Product product)
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM Products WHERE Id = {product.Id}";
                command.ExecuteNonQuery();
                Console.WriteLine($"{product.Name} has been removed.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error deleting product: {e.Message}");
                Console.WriteLine(UserInterface.divider);
            }
        }

        internal void UpdateProduct(Product product)
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = product.GetUpdateQuery();
                product.AddParameters(command);
                command.ExecuteNonQuery();
                Console.WriteLine("Product has been updated");
            }
            catch (Exception e)
            {
                Console.WriteLine($"There was an error updating product: {e.Message}");
                Console.WriteLine(UserInterface.divider);
            }
        }
    }
}

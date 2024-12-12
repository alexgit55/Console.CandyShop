using Microsoft.Data.Sqlite;
using static MarysCandyShop.Enums;

namespace MarysCandyShop.Models
{
    internal abstract class Product
    {
        internal string Name { get; set; }
        internal decimal Price { get; set; }

        internal int Id { get; }

        internal ProductType Type { get; set; }

        internal Product()
        {

        }

        public Product(int id)
        {
            Id = id;

        }
        // This method will be for formatting the product for the CSV file
        internal abstract string GetProductForCSV(int id);

        // This method will be for formatting the product for the ANSI Console panel for the View Product option
        internal abstract string GetProductForPanel();

        internal abstract string GetInsertQuery();

        internal abstract void AddParameters(SqliteCommand command);


    }

    internal class ChocolateBar : Product
    {
        internal int CocoaPercentage { get; set; }

        internal ChocolateBar()
        {
            Type = ProductType.ChocolateBar;
        }
        internal ChocolateBar(int id) : base(id)
        {
            Type = ProductType.ChocolateBar;
        }

        internal override string GetProductForCSV(int id)
        {
            return $"{id},{(int)Type},{Name},{Price},{CocoaPercentage}";
        }

        internal override string GetProductForPanel()
        {
            return $"Id: {Id}\nType: {Type}\nName: {Name}\nPrice: {Price}\nCocoa Percentage: {CocoaPercentage}";
        }

        internal override string GetInsertQuery()
        {
            return $@"INSERT INTO Products (name, price, type, cocoaPercentage) VALUES (@Name, @Price, @Type, @CocoaPercentage)";
        }

        internal override void AddParameters(SqliteCommand command)
        {
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@Type", (int)Type);
            command.Parameters.AddWithValue("@CocoaPercentage", CocoaPercentage);
        }
    }

    internal class Lollipop : Product
    {
        internal string Shape { get; set; }

        internal Lollipop()
        {
            Type = ProductType.Lollipop;
        }
        internal Lollipop(int id) : base(id)
        {
            Type = ProductType.Lollipop;
        }

        internal override string GetProductForCSV(int id)
        {
            return $"{id},{(int)Type},{Name},{Price},,{Shape}";
        }

        internal override string GetProductForPanel()
        {
            return $"Id: {Id}\nType: {Type}\nName: {Name}\nPrice: {Price}\nShape: {Shape}";
        }

        internal override string GetInsertQuery()
        {
            return $@"INSERT INTO Products (name, price, type, shape) VALUES (@Name, @Price, @Type, @Shape)";
        }

        internal override void AddParameters(SqliteCommand command)
        {
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@Type", (int)Type);
            command.Parameters.AddWithValue("@Shape", Shape);
        }
    }

}

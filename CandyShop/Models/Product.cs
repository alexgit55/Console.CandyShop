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
        // This method will be for formatting the product a spectre console table
        internal abstract string[] GetColumnsArray();

        // This method will be for formatting the product for the ANSI Console panel for the View Product option
        internal abstract string GetProductForPanel();

        internal abstract string GetInsertQuery();

        internal abstract void AddParameters(SqliteCommand command);

        internal abstract string GetUpdateQuery();


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

        internal override string GetProductForPanel()
        {
            return $"Id: {Id}\nName: {Name}\nType: {Type}\nPrice: {Price}\nCocoa Percentage: {CocoaPercentage}";
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

        internal override string[] GetColumnsArray()
        {
            return new string[] { Id.ToString(), Name, Type.ToString(), Price.ToString(), CocoaPercentage.ToString(), "" };
        }

        internal override string GetUpdateQuery()
        {
            return $"UPDATE Products SET name = @Name, price = @Price, type=0, cocoaPercentage = @CocoaPercentage WHERE id = {Id}";
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

        internal override string GetProductForPanel()
        {
            return $"Id: {Id}\nName: {Name}\nType: {Type}\nPrice: {Price}\nShape: {Shape}";
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

        internal override string[] GetColumnsArray()
        {
            return new string[] { Id.ToString(), Name, Type.ToString(), Price.ToString(), "", Shape };
        }

        internal override string GetUpdateQuery()
        {
            return $"UPDATE Products SET name = @Name, price = @Price, type=1, shape = @Shape WHERE id = {Id}";
        }
    }

}

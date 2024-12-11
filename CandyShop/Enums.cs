using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MarysCandyShop
{
    internal class Enums
    {
        internal enum MainMenuOptions
        {
            [Display(Name="View Products")]
            [Description("View All Products in the Shop")]
            ViewProducts,

            [Display(Name = "View Single Product")]
            [Description("View Detailed Information on a Single Product")]
            ViewSingleProduct,

            [Display(Name = "Add Product")]
            [Description("Add a Product to the Shop")]
            AddProduct,

            [Display(Name = "Delete Product")]
            [Description("Delete a Product from the Shop")]
            DeleteProduct,

            [Display(Name = "Update Product")]
            [Description("Modify a Product in the Shop")]
            UpdateProduct,

            [Display(Name = "Exit")]
            [Description("Close the program")]
            Exit
        }

        internal enum ProductType
        {
            ChocolateBar,
            Lollipop
        }

    }
}



using Resources.Interfaces;
using Resources.Models;

namespace MainApp.Menus;

public class Menu
{
    private readonly IProductService<Product,Product> _productService;

    private readonly ProductMenu _productMenu;

    public Menu(IProductService<Product, Product> productService, ProductMenu productMenu)
    {
        _productService = productService;
        _productMenu = productMenu;
    }


    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Main menu:");
            Console.WriteLine("__________\n");
            Console.WriteLine("1. Create Product.");
            Console.WriteLine("2. View All Products.");
            Console.WriteLine("3. View Single Product.");
            Console.WriteLine("4. Update Product.");
            Console.WriteLine("5. Delete Product.\n");

            Console.Write("Choose a menu: ");

            var menu = Console.ReadLine();

            switch (menu)
            {
                case "1":
                    _productMenu.CreateProduct();
                    break;
                case "2":
                    _productMenu.ViewAllMenu();
                    break;
                case "3":
                    _productMenu.GetSingleProduct();
                    break;
                case "4":
                    _productMenu.UpdateProduct();
                    break;
                case "5":
                    _productMenu.DeleteProduct();
                    break;
                default:
                    Console.WriteLine("\nInvalid menu. Press ENTER to try again!");
                    Console.ReadKey();
                    break;
            }
        }
    }
}

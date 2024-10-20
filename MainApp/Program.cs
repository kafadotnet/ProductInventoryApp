using Resources.Interfaces;
using Resources.Models;
using Resources.Services;
using System;

namespace MainApp;

class Program
{
    static void Main(string[] args)
    {
        IProductService<Product, Product> productService = new ProductService(@"C:\Projects\ProductInventoryApp\products.json");

        Console.ReadKey();
    }

}


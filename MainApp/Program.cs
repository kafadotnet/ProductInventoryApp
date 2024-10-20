
using Resources.Interfaces;
using Resources.Models;
using Resources.Services;
using MainApp.Menus;
using System;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp;

class Program
{
    private static IServiceProvider? _serviceProvider;

    static void Main(string[] args)
    {

        var serviceCollection = new ServiceCollection();

        ConfigureService(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();
        var menuService = _serviceProvider!.GetRequiredService<Menu>();

        menuService.MainMenu();

        Console.ReadKey();
    }

    private static void ConfigureService(IServiceCollection service)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDirectory, "products.json");

        service.AddSingleton<IProductService<Product, Product>, ProductService>();
        service.AddSingleton<IFileService>(new FileService(filePath));
        
        service.AddSingleton<Menu>();
        service.AddSingleton<ProductMenu>();
    }
}


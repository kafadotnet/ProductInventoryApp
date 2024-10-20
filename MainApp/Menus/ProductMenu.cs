
using Resources.Interfaces;
using Resources.Models;
using Resources.Services;

namespace MainApp.Menus;

public class ProductMenu
{
    private readonly IProductService<Product, Product> _productService;

    public ProductMenu(IProductService<Product, Product> productService)
    {
        _productService = productService;
    }

    public void CreateProduct()
    {
        
        //Instance accessible by every new product creation
        Product product = new Product();

        Category category = new Category();

        Console.Clear();
        Console.WriteLine("Create New Product\n");

        Console.Write("Enter Product Name: ");
        product.ProductName = Console.ReadLine() ?? "";

        Console.Write("Enter Product Specification: ");
        product.ProductSpecification = Console.ReadLine() ?? "";

        Console.Write("Enter Category Name: ");
        category.CategoryName = Console.ReadLine() ?? "";

        Console.Write("Enter Product Price: ");
        while (true)
        {
            if (decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                product.ProductPrice = price;
                break;
            }
            Console.WriteLine("Enter Product Price: ");
        }

        product.ProductCategory = category;
        var feedback = _productService.CreateProduct(product);

        if (feedback.Succeeded)
        {
            Console.WriteLine("\nProduct succefully created!");
        }
        else
        {
            Console.WriteLine($"\n{feedback.Message}");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();

    }

    public void ViewAllMenu()
    {
        var products = _productService.GetAllProducts().Feedback;

        Console.Clear();
        Console.WriteLine("View All Products:");
        Console.WriteLine("_________________\n");

        if (products != null)
        {
            foreach (Product product in products!)
            {
                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Specs: {product.ProductSpecification}");
                Console.WriteLine($"Category: {product.ProductCategory.CategoryName ?? ""}");
                Console.WriteLine($"Price: {product.ProductPrice}\n");
            }
        }
        else
        {
            Console.WriteLine("Product list is empty!\n");
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    public void GetSingleProduct()
    {
        var products = _productService.GetAllProducts().Feedback;

        Console.Clear();
        Console.WriteLine("View Single Product:");
        Console.WriteLine("_________________\n");

        if (products != null)
        {
            foreach (Product product in products!)
            {
                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Specs: {product.ProductSpecification}");
                Console.WriteLine($"Category: {product.ProductCategory.CategoryName ?? ""}");
                Console.WriteLine($"Price: {product.ProductPrice}\n");
            }
        }
        else
        {
            Console.WriteLine("Product list is empty!\n");
        }
        Console.Write("Copy ID to view Individual Product: " );
        var id = Console.ReadLine();

        if (!string.IsNullOrEmpty(id))
        {
            var singleProduct = _productService.GetSingleProduct(id.Trim());
            if (singleProduct.Succeeded && singleProduct.Feedback != null)
            {
                var product = singleProduct.Feedback;

                Console.WriteLine("The Product is: ");

                Console.WriteLine($"ID: {product!.Id}");
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Specs: {product.ProductSpecification}");
                Console.WriteLine($"Category: {product.ProductCategory.CategoryName ?? ""}");
                Console.WriteLine($"Price: {product.ProductPrice}\n");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    public void UpdateProduct()
    {
        var products = _productService.GetAllProducts().Feedback;

        Console.Clear();
        Console.WriteLine("Update Product:");
        Console.WriteLine("_________________\n");

        if (products != null)
        {
            foreach (Product product in products!)
            {
                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Specs: {product.ProductSpecification}");
                Console.WriteLine($"Category: {product.ProductCategory.CategoryName ?? ""}");
                Console.WriteLine($"Price: {product.ProductPrice}\n");
            }
        }
        else
        {
            Console.WriteLine("Product list is empty!\n");
        }
        Console.Write("Copy ID to update Individual Product: ");
        var id = Console.ReadLine();

        if (!string.IsNullOrEmpty(id))
        {
            var singleProduct = _productService.GetSingleProduct(id.Trim());
            if (singleProduct.Succeeded && singleProduct.Feedback != null)
            {
                var product = singleProduct.Feedback;

                Console.WriteLine("Change product name or price: ");

                Console.WriteLine("Do you want to change name?: (y/n)");

                string optionName = Console.ReadLine()!;

                if (optionName.ToLower() == "y")
                {
                    Console.WriteLine($"Name: {product.ProductName}");
                    product.ProductName = Console.ReadLine()!;
                }

                Console.WriteLine("Do you want to change price?: (y/n)");

                string optionPrice = Console.ReadLine()!;

                if (optionPrice.ToLower() == "y")
                {
                    Console.WriteLine($"Price: {product.ProductPrice}\n");

                    if (decimal.TryParse(Console.ReadLine(), out decimal price))
                    {
                        product.ProductPrice = price;
                    }
                }
                _productService.UpdateProduct(product);
                
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }

    public void DeleteProduct()
    {
        var products = _productService.GetAllProducts().Feedback;

        Console.Clear();
        Console.WriteLine("Delete Product:");
        Console.WriteLine("_________________\n");

        if (products != null)
        {
            foreach (Product product in products!)
            {
                Console.WriteLine($"ID: {product.Id}");
                Console.WriteLine($"Name: {product.ProductName}");
                Console.WriteLine($"Specs: {product.ProductSpecification}");
                Console.WriteLine($"Category: {product.ProductCategory.CategoryName ?? ""}");
                Console.WriteLine($"Price: {product.ProductPrice}\n");
            }
        }
        else
        {
            Console.WriteLine("Product list is empty!\n");
        }
        Console.Write("Copy ID to Delete Individual Product: ");
        var id = Console.ReadLine();

        if (!string.IsNullOrEmpty(id))
        {
            var singleProduct = _productService.GetSingleProduct(id.Trim());
            if (singleProduct.Succeeded && singleProduct.Feedback != null)
            {
                var product = singleProduct.Feedback;

                Console.Write($"Do you want to delete this product: {product.ProductName} (y/n) ");

                string optionName = Console.ReadLine()!;

                if (optionName.ToLower() == "y")
                {
                    _productService.DeleteProduct(id);
                    Console.WriteLine("Product deleted!");
                }
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}

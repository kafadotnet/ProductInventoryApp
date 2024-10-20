
using Newtonsoft.Json;
using Resources.Interfaces;
using Resources.Models;

namespace Resources.Services;

public class ProductService : IProductService<Product, Product>
{
    private readonly IFileService _fileService;
    private List<Product> _products;

    public ProductService(IFileService fileService)
    {
        _fileService = fileService;
        _products = GetAllProducts().Feedback!.ToList(); 
        
    }


    //Create Product
    public FeedbackStatus< Product> CreateProduct(Product product)
    {
        try
        {
            GetAllProducts();
            var exists = _products.FirstOrDefault(p => p.ProductName.ToLower() == product.ProductName.ToLower()); 
            
            if (exists != null)
            {
                return new FeedbackStatus<Product> { Succeeded = false, Message = "Product already exists!" };
            }

            _products.Add(product);

            var feedback = SaveToFile();
            if (feedback.Succeeded)
            {
                return new FeedbackStatus<Product> { Succeeded = true };
            }
            else
            {
                return new FeedbackStatus<Product> { Succeeded = false, Message = feedback.Message };
            }
        }
        catch (Exception ex)
        {
            return new FeedbackStatus<Product> { Succeeded = false, Message = ex.Message};
        }
    }

    //View All Products
    public FeedbackStatus<IEnumerable<Product>> GetAllProducts()
    {
        try
        {
            var feedback = _fileService.LoadFromFile();
            string content = feedback.Feedback!.ToString();

            if (feedback.Succeeded && !string.IsNullOrEmpty(content))
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(content);
                return new FeedbackStatus<IEnumerable<Product>> { Succeeded = true, Feedback = products };
            }
            else
            {
                return new FeedbackStatus<IEnumerable<Product>> { Succeeded = false, Message = feedback.Message };
            }
        }
        catch (Exception ex)
        {
            return new FeedbackStatus<IEnumerable<Product>> { Succeeded = false, Message = ex.Message };
        }
    }

    //View Single Product
    public FeedbackStatus<Product> GetSingleProduct(string id)
    {
        GetAllProducts();
        try
        {
            Product product = _products.FirstOrDefault(p => p.Id == id)!;
            return new FeedbackStatus<Product> { Succeeded = true, Feedback = product };
        }
        catch (Exception ex)
        {
            return new FeedbackStatus<Product> { Succeeded = false, Message = ex.Message };
        }
    }

    //Update Product
    public FeedbackStatus<Product> UpdateProduct(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            var index = _products.IndexOf(existingProduct);
            _products[index] = product;
            SaveToFile();

            return new FeedbackStatus<Product> { Succeeded = true  };
        }
        else
        {
            return new FeedbackStatus<Product> { Succeeded = false };

        }
    }

    //Delete Product
    public FeedbackStatus<Product> DeleteProduct(string id)
    {
        GetAllProducts();
        try
        {
            Product product = _products.FirstOrDefault(p => p.Id == id)!;
            if (product != null)
            {
                _products.Remove(product);
                var feedback = SaveToFile();
                return new FeedbackStatus<Product> { Succeeded = feedback.Succeeded };

            }
            return new FeedbackStatus<Product> { Succeeded = false};
        }
        catch (Exception ex)
        {
            return new FeedbackStatus<Product> { Succeeded = false, Message = ex.Message };
        }
    }

    private FeedbackStatus<Product> SaveToFile()
    {
        var json = JsonConvert.SerializeObject(_products, Formatting.Indented);
        var feedback = _fileService.SaveToFile(json);

        return new FeedbackStatus<Product> { Succeeded = true };
    }
}

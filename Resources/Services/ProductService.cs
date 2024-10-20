
using Newtonsoft.Json;
using Resources.Interfaces;
using Resources.Models;

namespace Resources.Services;

public class ProductService : IProductService<Product, Product>
{
    private readonly IFileService _fileService;
    private List<Product> _products;

    public ProductService(string filePath)
    {
        _fileService = new FileService(filePath);
        _products = new List<Product>();
        GetAllProducts();
    }


    //Create Product
    public FeedbackStatus< Product> CreateProduct(Product product)
    {
        try
        {
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

            if (feedback.Succeeded)
            {
                _products = JsonConvert.DeserializeObject<List<Product>>(feedback.Feedback!)!;
                return new FeedbackStatus<IEnumerable<Product>> { Succeeded = true, Feedback = _products };
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
    public FeedbackStatus<Product> UpdateProduct(string id, Product updatedProduct)
    {
        throw new NotImplementedException();
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

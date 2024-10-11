
using Resources.Interfaces;
using Resources.Models;

namespace Resources.Services;

public class ProductService : IProductService<Product, Product>
{
    public FeedbackStatus< Product> CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public FeedbackStatus<IEnumerable<Product>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public FeedbackStatus<Product> GetSingleProduct(string id)
    {
        throw new NotImplementedException();
    }

    public FeedbackStatus<Product> UpdateProduct(string id, Product updatedProduct)
    {
        throw new NotImplementedException();
    }

    public FeedbackStatus<Product> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }
}

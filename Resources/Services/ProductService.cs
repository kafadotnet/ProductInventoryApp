
using Resources.Interfaces;
using Resources.Models;

namespace Resources.Services;

public class ProductService : IProductService<Product, Product>
{
    //Create Product
    public FeedbackStatus< Product> CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    //View All Products
    public FeedbackStatus<IEnumerable<Product>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    //View Single Product
    public FeedbackStatus<Product> GetSingleProduct(string id)
    {
        throw new NotImplementedException();
    }

    //Update Product
    public FeedbackStatus<Product> UpdateProduct(string id, Product updatedProduct)
    {
        throw new NotImplementedException();
    }

    //Delete Product
    public FeedbackStatus<Product> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }
}

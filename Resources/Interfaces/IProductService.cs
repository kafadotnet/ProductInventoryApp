using Resources.Models;

namespace Resources.Interfaces;

public interface IProductService<T, TFeedback> where T : class where TFeedback : class
{
    FeedbackStatus<TFeedback> CreateProduct(T product);

    FeedbackStatus<IEnumerable<TFeedback>> GetAllProducts();

    FeedbackStatus<TFeedback> GetSingleProduct(String id);

    FeedbackStatus<TFeedback> UpdateProduct(string id, T updatedProduct);

    FeedbackStatus<TFeedback> DeleteProduct(String id);
}
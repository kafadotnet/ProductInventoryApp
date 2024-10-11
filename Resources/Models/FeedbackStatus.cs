
namespace Resources.Models;

public class FeedbackStatus<T> where T : class
{
    public bool Succeeded { get; set; }

    public string? Message { get; set; }

    public T? Feedback { get; set; }
}

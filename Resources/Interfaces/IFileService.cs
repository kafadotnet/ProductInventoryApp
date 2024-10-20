
using Resources.Models;

namespace Resources.Interfaces;

public interface IFileService
{
    public FeedbackStatus<string> SaveToFile (string content);

    public FeedbackStatus<string> LoadFromFile();
}

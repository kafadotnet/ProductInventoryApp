
using Resources.Interfaces;
using Resources.Models;

namespace Resources.Services;

public class FileService(string filePath) : IFileService
{
    private readonly string _filePath = filePath;

    public FeedbackStatus<string> LoadFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("File not found.");
            }

            using var sr = new StreamReader(_filePath);
            var content = sr.ReadToEnd();

            return new FeedbackStatus<string> { Succeeded = true, Feedback = content };
        }
        catch (Exception ex)
        {
            return new FeedbackStatus<string>
            {
                Succeeded = false,
                Message = ex.Message
            };
        }
    }

    public FeedbackStatus<string> SaveToFile(string content)
    {
        try
        {
            using var sw = new StreamWriter(_filePath, false);
            sw.WriteLine(content);

            return new FeedbackStatus<string> { Succeeded = true };
        }
        catch (Exception ex)
        {
            return new FeedbackStatus<string>
            {
                Succeeded = false,
                Message = ex.Message
            };
        }
    }
}

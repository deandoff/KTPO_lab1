namespace KTPO4317.Akhmetgaleev.Lib.LogAn;

public class WebService : IWebService
{
    public void LogError(string message)
    {
        Console.WriteLine($"Веб-служба: Логирование ошибки - {message}");
    }
}

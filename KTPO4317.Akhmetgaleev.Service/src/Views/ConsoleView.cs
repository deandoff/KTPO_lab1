using KTPO4317.Akhmetgaleev.Lib.LogAn;

namespace KTPO4317.Akhmetgaleev.Service.Views;

public class ConsoleView : IView
{
    public void Render(string text)
    {
        Console.WriteLine(text);
    }
}


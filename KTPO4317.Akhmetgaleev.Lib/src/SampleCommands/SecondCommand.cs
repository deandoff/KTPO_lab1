using KTPO4317.Akhmetgaleev.Lib.LogAn;

namespace KTPO4317.Akhmetgaleev.Lib.SampleCommands;

public class SecondCommand : ISampleCommand
{
    private readonly IView _view;

    public SecondCommand(IView view)
    {
        _view = view;
    }
    public void Execute()
    {
        _view.Render("Executing SecondCommand...");
        throw new InvalidOperationException("An error occurred in SecondCommand.");
    }
}


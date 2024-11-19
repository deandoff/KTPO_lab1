using KTPO4317.Akhmetgaleev.Lib.LogAn;

namespace KTPO4317.Akhmetgaleev.Lib.SampleCommands;

public class SampleCommandDecorator : ISampleCommand
{
    private readonly ISampleCommand? _command;
    private readonly IView? _view;

    public SampleCommandDecorator(ISampleCommand? command, IView? view)
    {
        _command = command;
        _view = view;
    }

    public void Execute()
    {
        _view?.Render("Начало: " + this.GetType().ToString());

        try
        {
            _command?.Execute();
        }
        finally
        {
            _view?.Render("Конец: " + this.GetType().ToString());
        }
    }
}
using KTPO4317.Akhmetgaleev.Lib.LogAn;

namespace KTPO4317.Akhmetgaleev.Lib.SampleCommands;

public class SampleExceptionDecorator : ISampleCommand 
{
    private readonly ISampleCommand _innerCommand;
    private readonly IView _view;

    public SampleExceptionDecorator(ISampleCommand innerCommand, IView view)
    {
        _innerCommand = innerCommand;
        _view = view;
    }

    public void Execute()
    {
        try
        {
            _innerCommand.Execute();
        }
        catch (Exception ex)
        {
            _view.Render($"Exception caught: {ex.Message}");
        }
    }
}
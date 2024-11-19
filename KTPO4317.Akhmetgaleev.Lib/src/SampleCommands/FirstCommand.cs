using KTPO4317.Akhmetgaleev.Lib.LogAn;

namespace KTPO4317.Akhmetgaleev.Lib.SampleCommands;

public class FirstCommand : ISampleCommand
{
    private readonly IView _view;
    private int iExecute = 0;
    public FirstCommand(IView view)
    {
        _view = view;
    }

    public void Execute()
    {
        _view.Render(this.GetType().ToString() + "\n iExecute = " + iExecute++);
    }
    
}
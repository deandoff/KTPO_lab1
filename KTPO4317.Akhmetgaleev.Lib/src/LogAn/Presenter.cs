namespace KTPO4317.Akhmetgaleev.Lib.LogAn;

public class Presenter
{
    private ILogAnalyzer? _analyzer;
    private IView? _view;
    
    public Presenter(ILogAnalyzer analyzer, IView view)
    {
        _analyzer = analyzer;
        _view = view;
        
        _analyzer.Analyzed += OnLogAnalyzed;
    }

    private void OnLogAnalyzed()
    {
        _view?.Render("Обработка завершена");
    }
}
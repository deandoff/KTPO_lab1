namespace KTPO4317.Akhmetgaleev.Lib.LogAn;

public interface ILogAnalyzer
{
    event LogAnalyzerAction Analyzed;
    bool IsValidLogFileName(string fileName);
    void Analyze(string fileName);
}
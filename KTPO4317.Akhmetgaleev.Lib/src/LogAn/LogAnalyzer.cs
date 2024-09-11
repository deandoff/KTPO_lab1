namespace KTPO4317.Akhmetgaleev.Lib.LogAn;

public class LogAnalyzer
{
    public bool WasLastFileNameValid { get; set; }
    public bool IsValidLogFileName(string fileName)
    {
        WasLastFileNameValid = false;
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("No file name provided");
        }
        WasLastFileNameValid = fileName.EndsWith(".AIA", StringComparison.CurrentCultureIgnoreCase);
        return WasLastFileNameValid;
    }
}
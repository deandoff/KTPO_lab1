using System.ComponentModel.DataAnnotations;

namespace KTPO4317.Akhmetgaleev.Lib.LogAn;

public class LogAnalyzer
{
    public LogAnalyzer(IExtensionManager extensionManager)
    {
        ExtensionManager = extensionManager;
    }

    private IExtensionManager ExtensionManager { get; set; }
    
    public bool IsValidLogFileName(string fileName)
    {
        try
        {
            return ExtensionManager.IsValid(fileName);
        }
        catch (Exception)
        {
            return false;
        }
    }
}
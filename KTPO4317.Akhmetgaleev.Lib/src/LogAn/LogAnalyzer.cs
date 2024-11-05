namespace KTPO4317.Akhmetgaleev.Lib.LogAn;

public class LogAnalyzer
{
    public bool IsValidLogFileName(string fileName)
    {
        try
        {
            IExtensionManager extensionManager = ExtensionManagerFactory.Create();
            return extensionManager.IsValid(fileName);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Analyze(string fileName)
    {
        if (fileName.Length < 8)
        {
            try
            {
                IWebService webService = WebServiceFactory.Create();
                webService.LogError("Слишком короткое имя файла: " + fileName);
            }
            catch (Exception e)
            {
                IEmailService emailService = EmailServiceFactory.Create();
                emailService.SendEmail("admin@example.com", "Невозможно вызвать веб-сервис", e.Message);
            } 
        }
    }
}
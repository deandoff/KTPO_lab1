namespace KTPO4317.Akhmetgaleev.Lib.LogAn;
using Microsoft.Extensions.Configuration;
using System.IO;

public class FileExtensionManager : IExtensionManager
{
    private string? _allowedExtension;

    public FileExtensionManager()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("config/appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();
        
        _allowedExtension = configuration["AllowedExtension"];
    }

    public bool IsValid(string fileName)
    {
        return _allowedExtension != null && fileName.EndsWith(_allowedExtension);
    }
}



namespace KTPO4317.Akhmetgaleev.Lib.LogAn;

public static class EmailServiceFactory
{
    private static IEmailService? _emailService;

    public static void SetService(IEmailService service)
    {
        _emailService = service;
    }

    public static IEmailService Create()
    {
        return _emailService ?? throw new NotImplementedException();
    }
}
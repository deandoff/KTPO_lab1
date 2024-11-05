namespace KTPO4317.Akhmetgaleev.Lib.LogAn;

public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}
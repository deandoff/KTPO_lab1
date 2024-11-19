using NUnit.Framework;
using KTPO4317.Akhmetgaleev.Lib.LogAn;
using NUnit.Framework.Legacy;

namespace KTPO4317.Akhmetgaleev.UnitTest.LogAn
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtension_True()
        { 
            FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
            fakeExtensionManager.WillBeValid = true;
         
            ExtensionManagerFactory.SetManager(fakeExtensionManager);
         
            LogAnalyzer logAnalyzer = new LogAnalyzer();
         
            bool result = logAnalyzer.IsValidLogFileName("short.ext");
         
            ClassicAssert.True(result);
        }

        [Test]
        public void IsValidFileName_NameUnsupportedExtension_False()
        {
            FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
            fakeExtensionManager.WillBeValid = false;

            ExtensionManagerFactory.SetManager(fakeExtensionManager);

            LogAnalyzer logAnalyzer = new LogAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName("unsupported.txt");

            ClassicAssert.False(result);
        }

        [Test]
        public void IsValidFileName_ExtensionManagerThrowsException_ReturnsFalse()
        {
            FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
            fakeExtensionManager.WillThrow = new Exception("Exception");

            ExtensionManagerFactory.SetManager(fakeExtensionManager);
            LogAnalyzer logAnalyzer = new LogAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName("file.ext");
            ClassicAssert.False(result);
        }

        [Test]
        public void Analyze_FileNameTooShort_CallsWebService()
        {
            FakeWebService mockWebService = new FakeWebService();
            WebServiceFactory.SetService(mockWebService);

            LogAnalyzer logAnalyzer = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            logAnalyzer.Analyze(tooShortFileName);

            if (mockWebService.Body != null)
                StringAssert.Contains("Слишком короткое имя файла: abc.ext", mockWebService.Body);
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            FakeWebService stubWebService = new FakeWebService();
            WebServiceFactory.SetService(stubWebService);
            stubWebService.WillThrow = new Exception("Это подделка");

            FakeEmailService mockEmailService = new FakeEmailService();
            EmailServiceFactory.SetService(mockEmailService);

            LogAnalyzer logAnalyzer = new LogAnalyzer();
            string tooShortFileName = "abc.txt";

            logAnalyzer.Analyze(tooShortFileName);

            if (mockEmailService.To != null) StringAssert.Contains("admin@example.com", mockEmailService.To);
            if (mockEmailService.Body != null) StringAssert.Contains("Это подделка", mockEmailService.Body);
            if (mockEmailService.Subject != null)
                StringAssert.Contains("Невозможно вызвать веб-сервис", mockEmailService.Subject);
        }

        [Test]
        public void Analyze_WhenAnalyzed_FiredEvent()
        {
            var analyzer = new LogAnalyzer();
            bool eventFired = false;
            analyzer.Analyzed += () => eventFired = true;
            
            analyzer.Analyze("validLogFile.txt");
            
            Assert.That(eventFired, Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            WebServiceFactory.SetService(null!);
            EmailServiceFactory.SetService(null!);
        }
    }

    internal class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid;

        public Exception? WillThrow;

        public bool IsValid(string fileName)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }

            return WillBeValid;
        }
    }

    internal class FakeWebService : IWebService
    {
        public string? Body;
        public Exception? WillThrow;

        public void LogError(string message)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }

            Body = message;
        }
    }

    internal class FakeEmailService : IEmailService
    {
        public string? To { get; private set; }
        public string? Subject { get; private set; }
        public string? Body { get; private set; }

        public void SendEmail(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
using NUnit.Framework;
using NSubstitute;
using KTPO4317.Akhmetgaleev.Lib.LogAn;

namespace KTPO4317.Akhmetgaleev.UnitTest.LogAn
{
    [TestFixture]
    public class LogAnalyzerNSubstituteTests
    {
        private IExtensionManager _fakeExtensionManager;
        private IWebService _mockWebService;
        private IEmailService _mockEmailService;

        [SetUp]
        public void SetUp()
        {
            _fakeExtensionManager = Substitute.For<IExtensionManager>();
            _mockWebService = Substitute.For<IWebService>();
            _mockEmailService = Substitute.For<IEmailService>();

            ExtensionManagerFactory.SetManager(_fakeExtensionManager);
            WebServiceFactory.SetService(_mockWebService);
            EmailServiceFactory.SetService(_mockEmailService);
        }

        [TearDown]
        public void TearDown()
        {
            ExtensionManagerFactory.SetManager(null!);
            WebServiceFactory.SetService(null!);
            EmailServiceFactory.SetService(null!);
        }

        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            _fakeExtensionManager.IsValid(Arg.Any<string>()).Returns(true);
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            bool result = logAnalyzer.IsValidLogFileName("validFile.log");
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsValidFileName_NameUnsupportedExtension_ReturnsFalse()
        {
            _fakeExtensionManager.IsValid(Arg.Any<string>()).Returns(false);
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            bool result = logAnalyzer.IsValidLogFileName("unsupported.txt");
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsValidFileName_ExtensionManagerThrowsException_ReturnsFalse()
        {
            _fakeExtensionManager
                .When(x => x.IsValid(Arg.Any<string>()))
                .Do(x => { throw new Exception("Exception"); });
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            bool result = logAnalyzer.IsValidLogFileName("file.ext");
            Assert.That(result, Is.False);
        }

        [Test]
        public void Analyze_FileNameTooShort_CallsWebService()
        {
            LogAnalyzer logAnalyzer = new LogAnalyzer();
            string tooShortFileName = "abc.ext";

            logAnalyzer.Analyze(tooShortFileName);
            
            _mockWebService.Received().LogError(Arg.Is<string>(s => 
                s.Contains("Слишком короткое имя файла: abc.ext")));
        }

        [Test]
        public void Analyze_WebServiceThrows_SendsEmail()
        {
            _mockWebService.When(x => x.LogError(Arg.Any<string>()))
                .Do(x => { throw new Exception("Это подделка"); });

            LogAnalyzer logAnalyzer = new LogAnalyzer();
            string tooShortFileName = "abc.txt";

            logAnalyzer.Analyze(tooShortFileName);
            
            _mockEmailService.Received().SendEmail(
                Arg.Is<string>(s => s == "admin@example.com"),
                Arg.Is<string>(s => s == "Невозможно вызвать веб-сервис"),
                Arg.Is<string>(s => s.Contains("Это подделка"))
            );
        }
    }
}

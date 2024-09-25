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
            
            LogAnalyzer logAnalyzer = new LogAnalyzer(fakeExtensionManager);

            bool result = logAnalyzer.IsValidLogFileName("short.ext");
            
            ClassicAssert.True(result);
        }

        [Test]
        public void IsValidFileName_NameUnsupportedExtension_False()
        {
            FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
            fakeExtensionManager.WillBeValid = false;

            LogAnalyzer logAnalyzer = new LogAnalyzer(fakeExtensionManager);

            bool result = logAnalyzer.IsValidLogFileName("unsupported.txt");

            ClassicAssert.False(result);
        }
        [Test]
        public void IsValidFileName_ExtensionManagerThrowsException_ReturnsFalse()
        {
            FakeExtensionManager fakeExtensionManager = new FakeExtensionManager();
            fakeExtensionManager.WillThrow = new Exception("Exception");

            LogAnalyzer logAnalyzer = new LogAnalyzer(fakeExtensionManager);
            
            bool result = logAnalyzer.IsValidLogFileName("file.ext");
            ClassicAssert.False(result);
        }
    }
    
    internal class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid = false;

        public Exception WillThrow = null;

        public bool IsValid(string fileName)
        {
            if (WillThrow != null)
            {
                throw WillThrow;
            }
            return WillBeValid;
        }
    }
}
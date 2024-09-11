using NUnit.Framework;
using KTPO4317.Akhmetgaleev.Lib.LogAn;
using NUnit.Framework.Legacy;

namespace KTPO4317.Akhmetgaleev.UnitTest.LogAn;

[TestFixture]
public class LogAnalyzerTests
{
    [Test]
    public void IsValidFileName_BadExtension_ReturnsFalse()
    {
        LogAnalyzer analyzer = new LogAnalyzer();
        
        bool result = analyzer.IsValidLogFileName("filewithbadextension.foo");
        
        Assert.That(result, Is.False);
    }   
    
    [Test]
    public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
    {
        LogAnalyzer analyzer = new LogAnalyzer();
        
        bool result = analyzer.IsValidLogFileName("filewithgoodextension.AIA");
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
    {
        LogAnalyzer analyzer = new LogAnalyzer();
        
        bool result = analyzer.IsValidLogFileName("filewithgoodextension.aia");
        
        Assert.That(result, Is.True);
    }

    [TestCase("filewithgoodextension.aia")]
    [TestCase("filewithgoodextension.AIA")]
    public void IsValidLogFileName_ValidExtension_ReturnsTrue(string filename)
    {
        LogAnalyzer analyzer = new LogAnalyzer();
        
        bool result = analyzer.IsValidLogFileName(filename);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValidLogFileName_EmptyFileName_ReturnsFalse()
    {
        LogAnalyzer analyzer = new LogAnalyzer();
        
        var exeption = Assert.Throws<ArgumentException>(() => analyzer.IsValidLogFileName(string.Empty));
        
        StringAssert.StartsWith(exeption.Message, "No file name provided");
    }

    [TestCase("badfile.foo", false)]
    [TestCase("goodfile.aia", true)]
    public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid(string filename, bool expected)
    {
        LogAnalyzer analyzer = new LogAnalyzer();
        
        analyzer.IsValidLogFileName(filename);
        
        ClassicAssert.AreEqual(expected, analyzer.WasLastFileNameValid);
    }
}
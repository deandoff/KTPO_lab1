using KTPO4317.Akhmetgaleev.Lib.LogAn;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4317.Akhmetgaleev.UnitTest.Sample;

public class SampleNSubstituteTests
{
    [Test]
    public void Returns_ParticularArg_Works()
    {
        IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();
        fakeExtensionManager.IsValid("validFile.ext").Returns(true);
        bool result = fakeExtensionManager.IsValid("validFile.ext");
        Assert.That(result, Is.True);
    }

    [Test]
    public void Returns_ArgAny_Works()
    {
        IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();
        fakeExtensionManager.IsValid(Arg.Any<String>()).Returns(true);
        bool result = fakeExtensionManager.IsValid("anyFile.ext");
        Assert.That(result, Is.True);
    }

    [Test]
    public void Returns_ArgAny_Throws()
    {
        IExtensionManager fakeExtensionManager = Substitute.For<IExtensionManager>();
        fakeExtensionManager
            .When(x => x.IsValid(Arg.Any<String>()))
            .Do(context => { throw new Exception("fake exception"); });
        Assert.Throws<Exception>(() => fakeExtensionManager.IsValid("anything"));
    }

    [Test]
    public void Received_ParticularArg_Saves()
    {
        IWebService mockWebService = Substitute.For<IWebService>();
        mockWebService.LogError("Поддельное сообщение");
        mockWebService.Received().LogError("Поддельное сообщение");
    }

    [Test]
    public void Analyze_TooShortFileName_CallsWebService()
    {
        
    }
}
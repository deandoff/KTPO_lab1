using KTPO4317.Akhmetgaleev.Lib.LogAn;
using KTPO4317.Akhmetgaleev.Lib.SampleCommands;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4317.Akhmetgaleev.UnitTest.Decorators;

[TestFixture]
public class SampleCommandDecoratorTests
{
    [Test]
    public void Execute_ShouldCallInnerCommandExecute()
    {
        var mockInnerCommand = Substitute.For<ISampleCommand>();
        var mockView = Substitute.For<IView>();
        var decorator = new SampleCommandDecorator(mockInnerCommand, mockView);
        
        decorator.Execute();
        
        mockInnerCommand.Received(1).Execute();
    }
    [Test]
    public void Execute_ShouldRenderTextFromDecorator()
    {
        var mockInnerCommand = Substitute.For<ISampleCommand>();
        var mockView = Substitute.For<IView>();
        var decorator = new SampleCommandDecorator(mockInnerCommand, mockView);

        decorator.Execute();
        
        mockView.Received(1).Render(Arg.Is<string>(s => s.Contains("Начало: KTPO4317.Akhmetgaleev.Lib.SampleCommands.SampleCommandDecorator")));
        mockView.Received(1).Render(Arg.Is<string>(s => s.Contains("Конец: KTPO4317.Akhmetgaleev.Lib.SampleCommands.SampleCommandDecorator")));
    }


}

using KTPO4317.Akhmetgaleev.Lib.LogAn;
using KTPO4317.Akhmetgaleev.Lib.SampleCommands;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4317.Akhmetgaleev.UnitTest.Decorators;

[TestFixture]
public class ExceptionHandlingDecoratorTests
{
    [Test]
    public void Execute_ShouldCallInnerCommandExecute()
    {
        var mockInnerCommand = Substitute.For<ISampleCommand>();
        var mockView = Substitute.For<IView>();
        var decorator = new SampleExceptionDecorator(mockInnerCommand, mockView);
        
        decorator.Execute();
        
        mockInnerCommand.Received(1).Execute();
    }
    [Test]
    public void Execute_ShouldHandleExceptionFromInnerCommand()
    {
        var mockInnerCommand = Substitute.For<ISampleCommand>();
        var mockView = Substitute.For<IView>();
        var decorator = new SampleExceptionDecorator(mockInnerCommand, mockView);
        
        mockInnerCommand.When(cmd => cmd.Execute()).Do(x => { throw new InvalidOperationException("Test exception"); });
        
        decorator.Execute();
        
        mockView.Received(1).Render(Arg.Is<string>(s => 
            s.Contains("Exception caught: Test exception")));
    }
}

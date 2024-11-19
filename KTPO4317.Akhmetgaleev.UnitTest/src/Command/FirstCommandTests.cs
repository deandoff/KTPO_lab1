using KTPO4317.Akhmetgaleev.Lib.LogAn;
using KTPO4317.Akhmetgaleev.Lib.SampleCommands;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4317.Akhmetgaleev.UnitTest.Command;

[TestFixture]
public class FirstCommandTests
{
    [Test]
    public void Execute_ShouldCallRenderWithCorrectText()
    {
        var mockView = Substitute.For<IView>();
        var command = new FirstCommand(mockView);
        
        command.Execute();
        
        mockView.Received(1).Render(Arg.Is<string>(s => 
            s.Contains("FirstCommand") && s.Contains("iExecute = 0")));
    }
}

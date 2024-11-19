using KTPO4317.Akhmetgaleev.Lib.LogAn;
using NSubstitute;
using NUnit.Framework;

namespace KTPO4317.Akhmetgaleev.UnitTest.LogAn
{
    [TestFixture]
    public class PresenterTests
    {
        private IView _mockView;
        private FakeLogAnalyzer _analyzer;
        private Presenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _mockView = Substitute.For<IView>();
            _analyzer = new FakeLogAnalyzer();
            _presenter = new Presenter(_analyzer, _mockView);
        }

        [TearDown]
        public void TearDown()
        {
            _mockView = null!;
            _analyzer = null!;
            _presenter = null!;
        }

        [Test]
        public void ctor_WhenAnalyzed_CallsViewRender()
        {
            _analyzer.CallRaiseAnalyzeEvent();
            
            _mockView.Received(1).Render(Arg.Is<string>(s => s == "Обработка завершена"));
        }

        [Test]
        public void ctor_WhenAnalyzed_CallsViewRender_NSubstitute()
        {
            var analyzer = Substitute.For<ILogAnalyzer>();
            _presenter = new Presenter(analyzer, _mockView);
            
            analyzer.Analyzed += Raise.Event<LogAnalyzerAction>();
            
            _mockView.Received(1).Render(Arg.Is<string>(s => s == "Обработка завершена"));
        }
        
        private class FakeLogAnalyzer : LogAnalyzer
        {
            public void CallRaiseAnalyzeEvent()
            {
                RaiseAnalyzedEvent();
            }
        }
    }
}
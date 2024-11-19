using KTPO4317.Akhmetgaleev.Lib.Common;
using KTPO4317.Akhmetgaleev.Lib.SampleCommands;
using KTPO4317.Akhmetgaleev.Service.WindsorInstallers;

namespace KTPO4317.Akhmetgaleev.Service
{
    public class Program
    {
        static void Main()
        {
            CastleFactory.Container?.Install(
                new SampleCommandInstaller(),
                new ViewInstaller());

            for (int i = 0; i < 3; i++)
            {
                ISampleCommand sampleCommand = CastleFactory.Container?.Resolve<ISampleCommand>()!;
                sampleCommand.Execute();
            }
        }
    }
}

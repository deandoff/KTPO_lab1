using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KTPO4317.Akhmetgaleev.Lib.SampleCommands;

namespace KTPO4317.Akhmetgaleev.Service.WindsorInstallers;

public class SampleCommandInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        
        container.Register(
            Component.For<ISampleCommand>()
                .ImplementedBy<SampleCommandDecorator>()
                .LifestyleSingleton(),
            Component.For<ISampleCommand>()
                .ImplementedBy<SampleExceptionDecorator>()
                .LifestyleSingleton(),
            Component.For<ISampleCommand>()
            .ImplementedBy<SecondCommand>()
            .LifestyleSingleton()
        );
    }
}
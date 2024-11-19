using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using KTPO4317.Akhmetgaleev.Lib.LogAn;
using KTPO4317.Akhmetgaleev.Service.Views;

namespace KTPO4317.Akhmetgaleev.Service.WindsorInstallers;

public class ViewInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(Component
            .For<IView>()
            .ImplementedBy<ConsoleView>()
            .LifestyleTransient()
        );
    }
}


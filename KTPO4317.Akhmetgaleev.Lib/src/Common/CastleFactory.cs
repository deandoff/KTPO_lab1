using Castle.Windsor;

namespace KTPO4317.Akhmetgaleev.Lib.Common;

public class CastleFactory
{
    public static IWindsorContainer? Container { get; private set; }

    static CastleFactory()
    {
        Container = new WindsorContainer();
    }
        
}
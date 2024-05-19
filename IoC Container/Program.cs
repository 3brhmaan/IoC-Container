using IoC_Container.Container;
using IoC_Container.Demo;

namespace IoC_Container;

internal class Program
{
    private static void Main(string[] args)
    {
        var iocContainer = new IoCContainer();

        iocContainer.Register<IWaterService, TapWaterService>();

        iocContainer.Register(typeof(IBeanService<>), typeof(ArabicaBeanService<>));

        iocContainer.Register<ICoffeeService, CoffeeService>();
        var coffeeService = iocContainer.Resolve<ICoffeeService>();

        Console.WriteLine(coffeeService.GetType());
    }
}
using IoC_Container.Container;
using IoC_Container.Demo;

namespace IoC_Container;

internal class Program
{
    private static void Main(string[] args)
    {
        var iocContainer = new IoCContainer();

        iocContainer.Register<IWaterService , TapWaterService>();
        var waterService = iocContainer.Resolve<IWaterService>();

        iocContainer.Register<ICoffeeService , CoffeeService>();
        var coffeeService = iocContainer.Resolve<ICoffeeService>();

        Console.WriteLine(waterService.GetType());
        Console.WriteLine(coffeeService.GetType());
    }
}
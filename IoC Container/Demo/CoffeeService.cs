namespace IoC_Container.Demo;

public class CoffeeService : ICoffeeService
{
    public CoffeeService(IWaterService waterService)
    {
    }

    public CoffeeService(IWaterService waterService,
        IBeanService<Catimor> beanService)
    {
    }
}
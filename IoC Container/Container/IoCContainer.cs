using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.Container;

public class IoCContainer
{
    Dictionary<Type , Type> _map = new Dictionary<Type , Type>();

    public void Register<TContract, TImplementation>()
        where TImplementation : class , TContract
        where TContract : class
    {
        if (!_map.ContainsKey(typeof(TContract)))
        {
            _map.Add(typeof(TContract) , typeof(TImplementation));
        }
    }

    public TContract Resolve<TContract>()
        where TContract : class
    {
        if (!_map.ContainsKey(typeof(TContract)))
        {
            throw new ArgumentException($"No Registration found for {typeof(TContract)}");
        }

        var implementation = _map[typeof(TContract)];

        return Create<TContract>(implementation);
    }

    private TContract Create<TContract>(Type implementationType)
        where TContract : class
    {
        return Activator.CreateInstance(implementationType, null) as TContract;
    }
}
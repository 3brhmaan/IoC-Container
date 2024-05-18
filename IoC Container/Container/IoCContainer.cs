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
    {
        if (!_map.ContainsKey(typeof(TContract)))
        {
            _map.Add(typeof(TContract) , typeof(TImplementation));
        }
    }
}
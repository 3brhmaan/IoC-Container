using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoC_Container.Container;

public class IoCContainer
{
    private Dictionary<Type, Type> _map = new();
    private MethodInfo? _resolveMethod;

    public void Register<TContract, TImplementation>()
        where TImplementation : class, TContract
        where TContract : class
    {
        if (!_map.ContainsKey(typeof(TContract))) _map.Add(typeof(TContract), typeof(TImplementation));
    }

    public void Register(Type contract, Type implementation)
    {
        if (!_map.ContainsKey(contract))
        {
            _map.Add(contract, implementation);
        }
    }

    public TContract Resolve<TContract>()
        where TContract : class
    {
        if (typeof(TContract).IsGenericType &&
            _map.ContainsKey(typeof(TContract)
                .GetGenericTypeDefinition()))
        {
            var openImplementation = _map[typeof(TContract).GetGenericTypeDefinition()];
            
            var closedImplementation = openImplementation
                .MakeGenericType(typeof(TContract).GenericTypeArguments);

            return Create<TContract>(closedImplementation);
        }

        if (!_map.ContainsKey(typeof(TContract)))
            throw new ArgumentException($"No Registration found for {typeof(TContract)}");

        var implementation = _map[typeof(TContract)];

        return Create<TContract>(implementation);
    }

    private TContract Create<TContract>(Type implementationType)
        where TContract : class
    {
        _resolveMethod ??= typeof(IoCContainer)
            .GetMethod("Resolve");

        var constructorParameters = implementationType.GetConstructors()
            .OrderByDescending(c => c.GetParameters().Length)
            .First()
            .GetParameters()
            .Select(p =>
            {
                var genericResolveMethod =
                    _resolveMethod?.MakeGenericMethod(p.ParameterType);

                return genericResolveMethod?.Invoke(this, null);
            })
            .ToArray();

        return Activator.CreateInstance(implementationType, constructorParameters) as TContract;
    }
}
using System;
using System.Collections.Generic;

public interface IGameService {
    public void PreDestroy();
}

public class ServiceLocator
{
    private static readonly Dictionary<string, IGameService> services = new Dictionary<string, IGameService>();

    private ServiceLocator() { }

    public static ServiceLocator Current { get; private set; }

    public static void Initialize()
    {
        Current = new ServiceLocator();
    }

    public T Get<T>() where T : IGameService
    {
        if (services.TryGetValue(typeof(T).Name, out var service))
            return (T)service;
        else
            throw new Exception($"Service of type {typeof(T).Name} not found in the locator.");
    }

    public void Register<T>(T service) where T : IGameService
    {
        services[typeof(T).Name] = service;
    }

    public void Unregister<T>() where T : IGameService
    {
        services.TryGetValue(typeof(T).Name, out var service);
        service.PreDestroy();
        services.Remove(typeof(T).Name);
    }
}

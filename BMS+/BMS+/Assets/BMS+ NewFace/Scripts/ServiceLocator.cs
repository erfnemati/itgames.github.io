using System;
using System.Collections.Generic;
using UnityEngine;

public interface IGameService {
    public void OnDisable();
}

public class ServiceLocator
{
    private Dictionary<string, IGameService> services = new Dictionary<string, IGameService>();

    private ServiceLocator() { }

    public static ServiceLocator _instance { get; private set; }

    public static void Initialize()
    {
        _instance = new ServiceLocator();
    }

    public T Get<T>() where T : IGameService
    {
        if (services.TryGetValue(typeof(T).Name, out var service))
            return (T)service;
        else
            throw new Exception($"Service of type {typeof(T).Name} not found in the locator.");
    }

    public void Register<T>(T service, GameObject gameObject) where T : IGameService
    {
        if (!services.ContainsKey(typeof(T).Name))
        {
            services[typeof(T).Name] = service;
            GameObject.DontDestroyOnLoad(gameObject);

        }
    }
    public void Register<T>(T service) where T : IGameService
    {
        if(!services.ContainsKey(typeof(T).Name))
            services[typeof(T).Name] = service;
    }

    public void Unregister<T>() where T : IGameService
    {
        services.TryGetValue(typeof(T).Name, out var service);
        service.OnDisable();
        services.Remove(typeof(T).Name);
    }
}

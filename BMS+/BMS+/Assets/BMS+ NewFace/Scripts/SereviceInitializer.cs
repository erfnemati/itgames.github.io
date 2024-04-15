using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SereviceInitializer : MonoBehaviour,IGameService
{
    private void Awake()
    {
        InitalizeServiceLocator();
        ServiceLocator._instance.Register<SereviceInitializer>(this, gameObject);
        InitializeService<EventManager>();
        InitializeService<PersistentDataManager>();
        InitializeService<PlayerLifeManager>();
        ServiceLocator._instance.Get<PersistentDataManager>();


    }

    private void InitalizeServiceLocator()=>ServiceLocator.Initialize();
    private void InitializeService<T>() where T : new() => new T();

    public void OnDestroy() { }
}

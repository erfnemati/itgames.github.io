using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SereviceInitializer : MonoBehaviour,IGameService
{
    [SerializeField] SoundManager soundManager;
    [SerializeField] DataManager dataManager;
    [SerializeField] BmsPlusSceneManager sceneManager;
    private void Awake()
    {
        InitalizeServiceLocator();
        ServiceLocator._instance.Register<SereviceInitializer>(this, gameObject);
        InitializeService<EventManager>();
        InitializeService<PersistentDataManager>();
        InitializeService<PlayerLifeManager>();
        ServiceLocator._instance.Register(soundManager);

    }

    private void InitalizeServiceLocator()=>ServiceLocator.Initialize();
    private void InitializeService<T>() where T : new() => new T();

    public void OnDestroy() { }
}

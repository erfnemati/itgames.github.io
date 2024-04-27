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
    [SerializeField] PersistentDataManager persistentDataManager;
    [SerializeField] ApiManager apiManager;
    private void Awake()
    {
        InitalizeServiceLocator();
        ServiceLocator._instance.Register<SereviceInitializer>(this, gameObject);
        InitializeService<EventManager>();
        InitializeService<PlayerLifeManager>();
        ServiceLocator._instance.Register(persistentDataManager);
        ServiceLocator._instance.Register(soundManager);
        ServiceLocator._instance.Register(apiManager);

    }

    private void InitalizeServiceLocator()=>ServiceLocator.Initialize();
    private void InitializeService<T>() where T : new() => new T();

    public void OnDestroy() { }
}

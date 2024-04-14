using ConfigData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameService
{
    [SerializeField] private PinConfig pinData;
    [SerializeField] private ShapeConfig shapeColor ;
    [SerializeField] private PrefabConfig prefabConfig ;
    [SerializeField] private SpriteConfig spriteConfig ;
    [SerializeField] private SoundConfig soundConfig ;
    [SerializeField] private GuideCanvasConfig guideCanvasConfig ;
    [SerializeField] private LevelsConfig levelsConfig ;

    // Start is called before the first frame update
    private void Awake()
    {
        ServiceLocator._instance.Register(this,gameObject);

    }
    public T GetData<T>(VectorInt color)
    {
        switch (typeof(T).Name)
        {
            case var type when type == typeof(PinConfigData).Name:
                return (T)(object)pinData.pins.Find(pin => pin.color == color);
            case var type when type == typeof(ShapeConfigData).Name:
                return (T)(object)shapeColor.shapeColors.Find(shape => shape.color == color);
            default:
                return (T)(object)null;
        }
    }

    public T GetData<T>(int id)
    {
        switch (typeof(T).Name)
        {
            case var type when type == typeof(PinConfigData).Name:
                foreach (PinConfigData pin in pinData.pins)
                    Debug.Log((int)pin.name==id);
                return (T)(object)pinData.pins.Find(pin => (int)pin.name == id);
            case var type when type == typeof(ShapeConfigData).Name:
                return (T)(object)shapeColor.shapeColors.Find(shape => (int)shape.name == id);
            case var type when type == typeof(SpriteConfigData).Name:
                return (T)(object)spriteConfig.sprites.Find(sprite => (int)sprite.name == id);
            case var type when type == typeof(SoundConfigData).Name:
                return (T)(object)soundConfig.sounds.Find(sound => (int)sound.name == id);
            case var type when type == typeof(GuideCanvasConfigData).Name:
                return (T)(object)guideCanvasConfig.guideCanvases.Find(guideCanvas => (int)guideCanvas.name == id);
            default: 
                return (T)(object)null;
        }
    }
    public T GetData<T>() 
    {

        switch (typeof(T).Name)
        {
            case var type when type == typeof(PinConfig).Name:
                return (T)(object)pinData;
            case var type when type == typeof(PrefabConfig).Name:
                return (T)(object)prefabConfig;
            case var type when type == typeof(SpriteConfig).Name:
                return (T)(object)spriteConfig;
            case var type when type == typeof(ShapeConfig).Name:
                return (T)(object)shapeColor;
            case var type when type == typeof(LevelsConfig).Name:
                return (T)(object)levelsConfig;
            default:
                return (T)(object)null;
        }
    }
    public void OnDisable() { }
}

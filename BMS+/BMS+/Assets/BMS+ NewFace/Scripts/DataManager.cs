using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager _instance;
    [SerializeField] private PinConfig pinData;
    [SerializeField] private ShapeColorConfig shapeColor ;
    [SerializeField] private PrefabConfig prefabConfig ;
    [SerializeField] private SpriteConfig spriteConfig ;
    [SerializeField] private SoundConfig soundConfig ;
    [SerializeField] private GuideCanvasConfig guideCanvasConfig ;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public T GetData<T>(Color color)
    {
        switch (typeof(T).Name)
        {
            case var type when type == typeof(PinColorData).Name:
                return (T)(object)pinData.pins.Find(pin => pin.color == color);
            case var type when type == typeof(ShapeColorData).Name:
                return (T)(object)shapeColor.shapeColors.Find(shape => shape.color == color);
            default:
                return (T)(object)null;
        }
    }

    public T GetData<T>(int id)
    {
        switch (typeof(T).Name)
        {
            case var type when type == typeof(PinColorData).Name:
                foreach (PinColorData pin in pinData.pins)
                    Debug.Log((int)pin.name==id);
                return (T)(object)pinData.pins.Find(pin => (int)pin.name == id);
            case var type when type == typeof(ShapeColorData).Name:
                return (T)(object)shapeColor.shapeColors.Find(shape => (int)shape.name == id);
            case var type when type == typeof(GameSpriteData).Name:
                return (T)(object)spriteConfig.sprites.Find(sprite => (int)sprite.name == id);
            case var type when type == typeof(SoundData).Name:
                return (T)(object)soundConfig.sounds.Find(sound => (int)sound.name == id);
            case var type when type == typeof(GuideCanvasDataa).Name:
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
            case var type when type == typeof(GameSpriteData).Name:
                return (T)(object)spriteConfig;
            case var type when type == typeof(ShapeColorConfig).Name:
                return (T)(object)shapeColor;
            default:
                return (T)(object)null;
        }
    }
}

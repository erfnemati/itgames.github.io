using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager _instance;
    [SerializeField] private PinConfig pinData;
    [SerializeField] private ShapeColorConfig shapeColor ;
    [SerializeField] private PrefabConfig prefabConfig ;
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

    public T GetColorData<T>(Color color)
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

    public T GetColorData<T>(int id)
    {
        switch (typeof(T).Name)
        {
            case var type when type == typeof(PinColorData).Name:
                return (T)(object)pinData.pins.Find(pin => pin.id == id);
            case var type when type == typeof(ShapeColorData).Name:
                return (T)(object)shapeColor.shapeColors.Find(shape => shape.id == id);
            default:
                return (T)(object)null;
        }
    }
    public PrefabConfig GetPrefabs()
    {
        return prefabConfig;
    }
}

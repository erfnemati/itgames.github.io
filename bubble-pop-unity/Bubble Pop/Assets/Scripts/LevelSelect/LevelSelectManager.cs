using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public static LevelSelectManager _levelSelectManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
        if(_levelSelectManagerInstance != null && _levelSelectManagerInstance != this)
        {
            _levelSelectManagerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionController : MonoBehaviour
{
    [SerializeField] private bool m_isStandVersion = true;
    public static VersionController _instance;

    private void Start()
    {
        if (_instance != null && _instance!= this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public bool IsStandVersion()
    {
        return m_isStandVersion;
    }

    
}

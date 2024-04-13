using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Change] this should be handled by buildmanager
public class VersionController : MonoBehaviour
{
    public static bool m_isStandVersion = false;
    public static VersionController _instance;

    private void Awake()
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

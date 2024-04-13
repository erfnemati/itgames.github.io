using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;


//[Change]: should be handled by buildmanager
public class ScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (VersionController._instance.IsStandVersion())
        {
            Debug.Log("Setting portrait resolution");
            SetPortraitResolution();
        }
        
    }

    private void SetPortraitResolution()
    {
        float nativeAspectRatio = 1920f / 1080f;
        int screenHeight = Screen.height;
        int screenWidth = (int)(screenHeight / nativeAspectRatio);
        Debug.Log(screenWidth);
        Screen.SetResolution(screenWidth, screenHeight, true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class ScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Screen.height < Screen.width)
        {
            SetPortraitResolution();
        }
        
    }

    private void SetPortraitResolution()
    {
        Debug.Log("Changing");
        float nativeAspectRatio = 1920f / 1080f;
        float screenHeight = Screen.height;
        float screenWidth = screenHeight / nativeAspectRatio;
        Screen.SetResolution(2160, 3840, true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonController : MonoBehaviour
{
    public void SkipLevel()
    {
        BmsPlusSceneManager._instance.LoadNextLevel();
    }
}

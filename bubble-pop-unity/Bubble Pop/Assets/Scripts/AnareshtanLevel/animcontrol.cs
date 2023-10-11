using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animcontrol : MonoBehaviour
{
    [SerializeField] GameObject thinklayer,sleeplayer,midlayer,grayback;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (thinklayer.active || sleeplayer.active || midlayer.active)
            Time.timeScale = 0.0f;
    //    else
       //     Time.timeScale = 1.0f;
    }
    public void startlevel()
    {
        grayback.SetActive(false);
        Time.timeScale = 1.0f;
    }
}

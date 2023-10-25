using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1learn : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject handguide;
    [SerializeField] GameObject dialougbox;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        overlay.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startlevel()
    {
        overlay.SetActive(false);
        Time.timeScale = 1.0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1learn : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject overlay2;
    [SerializeField] GameObject txtpart1;
    [SerializeField] GameObject txtpart2;
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

    public void next()
    {
        txtpart1.SetActive(false);
        txtpart2.SetActive(true);
    }
   public void nexttopopbuble()
    {
       // overlay2.SetActive(false);
        txtpart2.SetActive(false);
        dialougbox.SetActive(false);
    }
    public void startlevel()
    {
        overlay.SetActive(false);
        Time.timeScale = 1.0f;
    }
}

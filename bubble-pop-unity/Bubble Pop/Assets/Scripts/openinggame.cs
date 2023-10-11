using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openinggame : MonoBehaviour
{
    [SerializeField] GameObject roban1;
    [SerializeField] GameObject roban2;
    [SerializeField] GameObject openingvfx;
    // [SerializeField] GameObject dialougept1;
    //[SerializeField] GameObject dialougept2;
    [SerializeField] GameObject blockedpanel;
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject dialb;
    [SerializeField] AudioSource openingdone;
    
    // Start is called before the first frame update
    void Start()
    {
        overlay.SetActive(true);
        blockedpanel.SetActive(true);
       // overlay.SetActive(true);
       // dialougept1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialb.active)
            blockedpanel.SetActive(false);
    }
    public void res()
    {
      //  overlay.SetActive(false);
      //  dialougept1.SetActive(false);
       // SceneManager.LoadScene("");

        // Time.timeScale = 1.0f;

    }
    public void open()
    {
        openingdone.Play();
        roban1.SetActive(false);
        roban2.SetActive(false);
        openingvfx.SetActive(true);
       // dialougept2.SetActive(true);
        SceneManager.LoadScene("Anarestan");

    }
    public void nextlevel()
    {
        //SceneManager.LoadScene("");
    }
}

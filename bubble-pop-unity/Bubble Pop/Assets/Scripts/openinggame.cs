using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openinggame : MonoBehaviour
{
    [SerializeField] GameObject roban1;
    [SerializeField] GameObject roban2;
    [SerializeField] GameObject openingvfx;
    [SerializeField] GameObject btn;
    //[SerializeField] GameObject dialougept2;
    [SerializeField] GameObject blockedpanel;
    [SerializeField] GameObject overlay;
    [SerializeField] GameObject dialb;
    [SerializeField] GameObject syaremanlogo;
    [SerializeField] AudioSource popbtnsound;
    [SerializeField] AudioSource levelsound;
    [SerializeField] AudioSource openingdone;
    [SerializeField] AudioSource showlogo;
    
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
        popbtnsound.Play();
        openingdone.Play();
        btn.SetActive(false);
        roban1.SetActive(false);
        roban2.SetActive(false);
        openingvfx.SetActive(true);
        levelsound.Stop();
        // dialougept2.SetActive(true);
        Foo();
        
    }

    public void Foo()
    {
        StartCoroutine(TemporarilyDeactivate(6.0f));
    }

    private IEnumerator TemporarilyDeactivate(float duration)
    {
        yield return new WaitForSeconds(3.0f);
        showlogo.Play();
        syaremanlogo.SetActive(true);
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene("preAnarestan");
        Debug.Log("endtimer");
        
    }

}

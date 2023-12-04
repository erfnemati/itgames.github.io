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
    [SerializeField] GameObject handguide;
    //[SerializeField] GameObject overlay;
    [SerializeField] GameObject dialougebox;
    [SerializeField] GameObject firsttxt;
    [SerializeField] GameObject secondtxt;
    [SerializeField] GameObject realbublebtn;
    [SerializeField] GameObject fakebublebtn;
    //[SerializeField] GameObject dialb;
    [SerializeField] GameObject syaremanlogo;
    [SerializeField] AudioSource popbtnsound;
    [SerializeField] AudioSource levelsound;
    [SerializeField] AudioSource openingdone;
    [SerializeField] AudioSource showlogo;
    [SerializeField] GameObject m_levelSelectorManager;
    

    // Start is called before the first frame update
    void Start()
    {
        //overlay.SetActive(true);
        //blockedpanel.SetActive(true);
       // overlay.SetActive(true);
       // dialougept1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!dialb.active)
        //    blockedpanel.SetActive(false);
    }
    public void res()
    {
        //  overlay.SetActive(false);
        //  dialougept1.SetActive(false);
        // SceneManager.LoadScene("");
        firsttxt.SetActive(false);
        secondtxt.SetActive(true);
        // Time.timeScale = 1.0f;

    }

    public void resu2()
    {
        secondtxt.SetActive(false);
        dialougebox.SetActive(false);
        handguide.SetActive(true);
       // overlay.SetActive(false);
        realbublebtn.SetActive(true);
        fakebublebtn.SetActive(false);
    }

    public void open()
    {
        handguide.SetActive(false);
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
        UpdateLevelSelectManager();
        yield return new WaitForSeconds(3.0f);
        showlogo.Play();
        syaremanlogo.SetActive(true);
        yield return new WaitForSeconds(duration);
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = (currentLevelIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextLevelIndex);
        Debug.Log("endtimer");
        
    }

    private void UpdateLevelSelectManager()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current level index is : " + currentLevelIndex);

        LevelInfo tempLvlInfo = new LevelInfo(3, true);

        if (LevelSelectManager._levelSelectManagerInstance != null)
        {
            LevelSelectManager._levelSelectManagerInstance.UpdateLevelInfo(currentLevelIndex, tempLvlInfo);
        }
        else
        {
            Instantiate(m_levelSelectorManager);
            LevelSelectManager._levelSelectManagerInstance.UpdateLevelInfo(currentLevelIndex, tempLvlInfo);
        }
        LevelSelectManager._levelSelectManagerInstance.UpdateCurrentLevel
            ((SceneManager.GetActiveScene().buildIndex + 1));

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreandtimerlevel1 : MonoBehaviour
{
    public float timer=0,totaltime;
    public int score=0;
    
    // Start is called before the first frame update
    void Start()
    {
        score += PlayerPrefs.GetInt("totalstars");
        timer += PlayerPrefs.GetFloat("level1timer");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        PlayerPrefs.SetFloat("level1timer",timer);
       // PlayerPrefs.SetFloat("totaltime", totaltime);
        PlayerPrefs.SetInt("totalstars", score);
    }
    public void scorecounter(int stars)
    {
        score+=stars;
       // PlayerPrefs.SetInt("starss", stars);
    }
}

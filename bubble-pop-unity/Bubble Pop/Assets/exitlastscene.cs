using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitlastscene : MonoBehaviour
{
    public void changescene()
    {
        SceneManager.LoadScene("Start Screen");
    }
   
}

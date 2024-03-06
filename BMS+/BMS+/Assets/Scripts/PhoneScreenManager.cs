using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneScreenManager : MonoBehaviour
{

    public static string _phoneScreenLevelName = "PhoneScreen";
    public delegate void TheEnd();
    public static event TheEnd EndLevel;

    // Start is called before the first frame update
    private void Start()
    {
        EndLevel();
    }

    public void QuitFromPhoneScreen()
    {
        PersistentDataManager persistentDataManager = FindObjectOfType<PersistentDataManager>();
        if (persistentDataManager != null)
        {
            Destroy(persistentDataManager);
        }

        SceneManager.LoadScene(0);
    }




}

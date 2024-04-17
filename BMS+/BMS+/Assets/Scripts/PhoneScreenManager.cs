using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneScreenManager : MonoBehaviour
{

    public static string _phoneScreenLevelName = "PhoneScreen";


    // Start is called before the first frame update
    private void Start()
    {
        ServiceLocator._instance.Get<EventManager>().TriggerEvent(EventName.TheEnd);
    }

    public void QuitFromPhoneScreen()
    {
        //PersistentDataManager persistentDataManager = FindObjectOfType<PersistentDataManager>();
        //if (persistentDataManager != null)
        //{
        //    Destroy(persistentDataManager);
        //}

        ServiceLocator._instance.Get<BmsPlusSceneManager>().LoadScene(SceneName.MainMenu);
    }




}

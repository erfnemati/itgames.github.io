using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoshanbeSuriGameManager : MonoBehaviour
{
    public static DoshanbeSuriGameManager _instance;
    [SerializeField] GameObject m_grayScreen;
    [SerializeField] GameObject m_resultMenu;

    private void Start()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void FinishGame()
    {
        m_grayScreen.gameObject.SetActive(true);
        m_resultMenu.gameObject.SetActive(true);
    }
}

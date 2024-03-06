using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;
using TMPro;


public class SendButtonHandler : MonoBehaviour
{
    [SerializeField] TextInputManager m_textInputManager;
    [SerializeField] RTLTextMeshPro m_errorText;

    public void SavePhoneNumber()
    {
        
        if (m_textInputManager.ValidateText())
        {
            PersistentDataManager._instance.SaveData(m_textInputManager.GetConvertedString());
            GetComponent<Button>().gameObject.SetActive(false);
            Invoke(nameof(LoadNextLevel), 0.5f);
        }
        else
        {
            TryAgain();
        }
        
    }

    public void TryAgain()
    {
        m_textInputManager.RefreshInput();
        ActivateError();
    }

    public void ActivateError()
    {
        m_errorText.gameObject.SetActive(true);
        Invoke(nameof(DeactivateErrorText), 2f);
    } 
    
    public void DeactivateErrorText()
    {
        m_errorText.gameObject.SetActive(false);
    }

    public void LoadNextLevel()
    {
        BmsPlusSceneManager._instance.LoadNextLevel();
    }


}

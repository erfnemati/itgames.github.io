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
    [SerializeField] RTLTextMeshPro m_phoneNumber;
    [SerializeField] TMP_InputField m_InputField;

    public void Start()
    {
        m_textInputManager = GetComponent<TextInputManager>();
    }
    public void SavePhoneNumber()
    {
        string convertedString = m_textInputManager.ConvertText(m_phoneNumber.text);
        if (m_textInputManager.ValidateText(convertedString))
        {
            PersistentDataManager._instance.SaveData(convertedString);
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
        m_errorText.gameObject.SetActive(true);
        m_InputField.Select();
        m_InputField.text = "";
        Invoke(nameof(ClearErrorText), 1.5f);
    }

    public void ClearErrorText()
    {
        m_errorText.gameObject.SetActive(false);
    }

    public void LoadNextLevel()
    {
        BmsPlusSceneManager._instance.LoadNextLevel();
    }


}

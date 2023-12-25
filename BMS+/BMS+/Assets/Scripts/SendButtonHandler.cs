using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;
using TMPro;


public class SendButtonHandler : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_errorText;
    [SerializeField] RTLTextMeshPro m_phoneNumber;
    [SerializeField] TMP_InputField m_InputField;
 
    public void SavePhoneNumber()
    {
        PersistentDataManager._instance.SaveData(m_phoneNumber.text);
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


}

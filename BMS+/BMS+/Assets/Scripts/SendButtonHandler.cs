using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class SendButtonHandler : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_phoneNumber;

    public void SavePhoneNumber()
    {
        Debug.Log("Saving phone number : " + m_phoneNumber);
        PersistentDataManager._instance.SaveData(m_phoneNumber.text);
    }
}

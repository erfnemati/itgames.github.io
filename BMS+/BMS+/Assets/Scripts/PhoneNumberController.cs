using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class PhoneNumberController : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_phoneNumberText;
    [SerializeField] Stack<char> m_phoneNumber = new Stack<char>();


    private void UpdatePhoneNumberText()
    {
        m_phoneNumberText.color = Color.white;
        char[] tempArray = new char[m_phoneNumber.Count];
        int tempArrayIndex = 0;

        foreach(char tempChar in m_phoneNumber)
        {
            tempArray[tempArrayIndex] = tempChar;
            tempArrayIndex++;
        }

        m_phoneNumberText.text = new string(tempArray);
    }

    public void AddDigit(char digit)
    {
        m_phoneNumber.Push(digit);
        UpdatePhoneNumberText();
    }

    public void RemoveDigit()
    {
        if (m_phoneNumber.Count != 0)
        {
            m_phoneNumber.Pop();
            UpdatePhoneNumberText();
        }

        
    }
}

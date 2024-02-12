using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using RTLTMPro;
using System.Text.RegularExpressions;

public class TextInputManager : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_phoneNumberText;
    [SerializeField] Stack<char> m_phoneNumberStack = new Stack<char>();
    [SerializeField] int m_maxNumberOfChars = 11;

    private string m_convertedString;
    private char[] m_englishNumbers = new char[] { '0','1', '2', '3', '4', '5', '6', '7', '8', '9' };


    public bool ValidateText()
    {
        ConvertText();
        Regex reg = new Regex("^(\\+98|0)?9\\d{9}$");
        if (reg.IsMatch(m_convertedString))
        {
            return true;
        }

        return false;
    }

    public string ConvertText()
    {
        
        char[] tempCharString = m_phoneNumberText.text.ToCharArray();
        char[] secondTempCharStr = new char [m_phoneNumberText.text.Length - 1];

        for (int i = 0; i < tempCharString.Length - 1 ;i++)
        {
            tempCharString[i] = m_englishNumbers[(tempCharString[i] - 0x06F0)];
            secondTempCharStr[secondTempCharStr.Length - 1 - i] = tempCharString[i];
        }
        m_convertedString = new string(secondTempCharStr);
        return m_convertedString;
    }

    public string GetConvertedString()
    {
        return m_convertedString;
    }


    private void UpdatePhoneNumberText()
    {
        m_phoneNumberText.color = Color.white;
        List<char> tempArray = new List<char>();
        

        foreach (char tempChar in m_phoneNumberStack)
        {
            tempArray.Add(tempChar);
            
        }

        tempArray.Reverse();
        m_phoneNumberText.text = new string(tempArray.ToArray());
    }

    public void AddDigit(char digit)
    {
        if (m_phoneNumberStack.Count < 11)
        {
            m_phoneNumberStack.Push(digit);
            UpdatePhoneNumberText();
        }
    }

    public void RemoveDigit()
    {
        if (m_phoneNumberStack.Count != 0)
        {
            m_phoneNumberStack.Pop();
            UpdatePhoneNumberText();
        }


    }

    public void RefreshInput()
    {
        m_phoneNumberText.color = new Color32(255,255,255,76);
        m_phoneNumberText.text = "09125281416";
        m_phoneNumberStack.Clear();
    }

    public string GetEnteredText()
    {
        return m_phoneNumberText.text;
    }



}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using RTLTMPro;
using System.Text.RegularExpressions;

public class TextInputManager : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_inputText;
    private string m_convertedString;
    private char[] m_englishNumbers = new char[] { '0','1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private bool ValidateText()
    {
        Debug.Log(m_convertedString);
        Regex reg = new Regex("^(\\+98|0)?9\\d{9}$");
        return reg.IsMatch(m_convertedString);

    }

    private void ConvertText()
    {
        char[] tempCharString = m_inputText.text.ToCharArray();
        char[] secondTempCharStr = new char[m_inputText.text.Length - 1];
        int j = 0;

        for (int i = 0; i < tempCharString.Length - 1 ;i++)
        {
            tempCharString[i] = m_englishNumbers[(tempCharString[i] - 0x06F0)];
            secondTempCharStr[secondTempCharStr.Length - 1 - i] = tempCharString[i];
        }
        m_convertedString = new string(secondTempCharStr);
        
    }

    public void SaveText()
    {
        ConvertText();

        if (ValidateText())
        {
            Debug.Log("Time to save");
        }
        else
        {
            Debug.Log("Not validated");
        }
    } 
}

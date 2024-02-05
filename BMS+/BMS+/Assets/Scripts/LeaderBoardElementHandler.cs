using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class LeaderBoardElementHandler : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_rank;
    [SerializeField] RTLTextMeshPro m_phoneNumber;
    [SerializeField] RTLTextMeshPro m_lastPassedLevel;
    [SerializeField] RTLTextMeshPro m_passedTime;


    public void SetElements(int rank,string phoneNumber,int lastPassedLevel,float passedTime)
    {
        m_rank.text = rank + "";
        m_phoneNumber.text = ExtractPhoneNumber(phoneNumber);
        m_lastPassedLevel.text = lastPassedLevel.ToString();
        m_passedTime.text = ExtractTimeInRightForm(passedTime);
    }

    private string ExtractTimeInRightForm(float passedTime)
    {
        int minutes = (int) passedTime / 60;
        int seconds = ((int)passedTime) % 60;

        string finalString = seconds + ":" + minutes;
        return finalString;
    }

    private string ExtractPhoneNumber(string phoneNumber)
    {
        char[] phoneNumberChars = phoneNumber.ToCharArray();
        char[] finalPhoneNumberChars = new char[phoneNumberChars.Length + 1];
        for (int i = 0; i < phoneNumberChars.Length;i++)
        {
            if (i==4 || i==5 || i==6 || i==7)
            {
                finalPhoneNumberChars[i] = '*';
            }
            else
            {
                finalPhoneNumberChars[i] = phoneNumberChars[i];
            }
        }

        string finalString = new string(finalPhoneNumberChars);
        return finalString;
    }

}

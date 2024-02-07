using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using UnityEngine.UI;

public class LeaderBoardElementHandler : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_rank;
    [SerializeField] RTLTextMeshPro m_phoneNumber;
    [SerializeField] RTLTextMeshPro m_lastPassedLevel;
    [SerializeField] RTLTextMeshPro m_passedTime;

    [SerializeField] GameObject m_goldMedal;
    [SerializeField] GameObject m_silverMedal;
    [SerializeField] GameObject m_bronzeMedal;

    


    public void SetElements(int rank,string phoneNumber,int lastPassedLevel,float passedTime)
    {
        
        m_phoneNumber.text = ExtractPhoneNumber(phoneNumber);
        m_lastPassedLevel.text = lastPassedLevel.ToString();
        m_passedTime.text = ExtractTimeInRightForm(passedTime);
        if (rank == 1|| rank == 2 || rank==3)
        {
            SetMedal(rank);
        }
        else
        {
            m_rank.gameObject.SetActive(true);
            m_rank.text = rank + "";
        }
    }

    private void SetMedal(int rank)
    {
        if (rank == 1)
        {
            m_goldMedal.gameObject.SetActive(true);
        }
        else if (rank == 2)
        {
            m_silverMedal.gameObject.SetActive(true);
        }
        else
        {
            m_bronzeMedal.gameObject.SetActive(true);
        }
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

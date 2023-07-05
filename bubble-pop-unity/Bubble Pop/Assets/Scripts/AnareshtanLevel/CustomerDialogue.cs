using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerDialogue : MonoBehaviour
{
    [SerializeField] TMP_Text m_dialogueBox;



    private void Start()
    {
        if (DialogueManager.m_DialogueManagerInstance == null)
        {
            Debug.Log("Null found here");
        }
        ShowNextDialogue("Tap To Conatinue");
    }
    public void ShowNextDialogue(string additionalWords)
    {
        Debug.Log("Additional words are : " +additionalWords);
        m_dialogueBox.text = DialogueManager.m_DialogueManagerInstance.GetNextCustomerDialogue() + "\n" + "("+additionalWords + ")" ;
    }

    
}

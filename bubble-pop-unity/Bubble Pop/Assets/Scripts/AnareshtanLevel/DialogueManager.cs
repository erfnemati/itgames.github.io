using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static  DialogueManager m_DialogueManagerInstance;


    [SerializeField]List<string> m_customerDialogues = new List<string>();
    private int m_customerDialogueIndex = 0;


    [SerializeField]List<string> m_ingameDialogues = new List<string>();
    private int m_ingameDialogueIndex = 0;


    private void Awake()
    {
        if (m_DialogueManagerInstance == null)
        {
            m_DialogueManagerInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public  string GetNextCustomerDialogue()
    {
        string nextDialogue = m_customerDialogues[m_customerDialogueIndex];
        m_customerDialogueIndex++;
        Debug.Log("Index dialogue is : " + m_customerDialogueIndex);
        if(m_customerDialogueIndex > m_customerDialogues.Count)
        {
            Debug.Log("Start repeating customer dialogues");
            m_customerDialogueIndex = 0;
        }
        return nextDialogue;
    }

    public string GetNextIngameDialogue()
    {
        string nextDialogue = m_ingameDialogues[m_ingameDialogueIndex];
        if (m_ingameDialogueIndex > m_ingameDialogues.Count)
        {
            Debug.Log("Start repeating ingame dialogues");
            m_ingameDialogueIndex = 0;
             
        }
        return nextDialogue;
    }
}

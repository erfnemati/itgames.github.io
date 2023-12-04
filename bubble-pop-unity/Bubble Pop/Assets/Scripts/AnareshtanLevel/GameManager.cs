using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] int m_anarSequeneceIndex;
    [SerializeField] GameObject m_anarBubble;
    [SerializeField] GameObject m_anarBubbleTapText;
    private int sequenceIndex = 1;


    [SerializeField]CustomerDialogue m_customerDialogue;

    public static GameManager m_instance;

    

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (sequenceIndex <m_anarSequeneceIndex )
            {
                m_customerDialogue.ShowNextDialogue(new string("Tap To Help"));
                
            }

            else if (sequenceIndex == m_anarSequeneceIndex)
            {
                m_anarBubble.SetActive(true);
                m_anarBubbleTapText.SetActive(true);
                m_customerDialogue.gameObject.SetActive(false);
            }

            sequenceIndex++;
        }
    }



}



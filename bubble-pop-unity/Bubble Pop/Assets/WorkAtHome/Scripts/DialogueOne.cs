using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

namespace Assets.WorkAtHome.Scripts
{
    class DialogueOne : MonoBehaviour, ISequence
    {
        [SerializeField] string m_dialogueText;
        [SerializeField] GameObject m_dialogueBox;
        private TMP_Text m_customerDialogueBoxText;

        [SerializeField] float m_finalDialogueBoxScale;
        [SerializeField] float m_dialogueBoxScaleTime;
        
        public void ChangeSequence()
        {
            Debug.Log("First dialogues has been finished");
            TutorialManager.m_instance.GoNextSequence();
        }

        public void PlaySequence()
        {
            m_dialogueBox.gameObject.SetActive(true);
            m_dialogueBox.transform.DOScale(m_finalDialogueBoxScale, m_dialogueBoxScaleTime).OnComplete(()=>ShowCustomerDialogue());
            
        }

        public void ShowCustomerDialogue()
        {
            m_customerDialogueBoxText = m_dialogueBox.GetComponentInChildren<TextMeshPro>();
            if (m_customerDialogueBoxText != null)
            {
                m_customerDialogueBoxText.text = m_dialogueText;
            }
            else
            {
                Debug.Log("dialogue box is empty");
            }
        }

        
    }
}

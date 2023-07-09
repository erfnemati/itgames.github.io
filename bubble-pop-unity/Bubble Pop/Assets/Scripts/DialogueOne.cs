using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Assets.WorkAtHome.Scripts
{
    class DialogueOne : MonoBehaviour, ISequence
    {
        [SerializeField] string m_dialogueText;
        [SerializeField] GameObject m_dialogueBox;
        private TMP_Text m_customerDialogueBoxText;

        [SerializeField] float m_finalDialogueBoxScale;
        [SerializeField] float m_dialogueBoxScaleTime;
        //[SerializeField] Button m_triggerButton;
        
        public void ChangeSequence()
        {
            Debug.Log("Here we go again");
            
            TutorialManager.m_instance.GoNextSequence();
            //m_triggerButton.onClick.RemoveAllListeners();


        }

        public void PlaySequence()
        {
            
            m_dialogueBox.gameObject.SetActive(true);
            //m_triggerButton.onClick.AddListener(()=>this.ChangeSequence());
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

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Assets.WorkAtHome.Scripts
{
    class DialogueTwo : MonoBehaviour, ISequence
    {
        [SerializeField] string m_dialogueText;
        [SerializeField] GameObject m_dialogueBox;
        //[SerializeField] Button m_triggerButton;

        private TMP_Text m_customerDialogueBoxText;
        public void ChangeSequence()
        {
            Debug.Log("Number of repetetion");
            m_dialogueBox.gameObject.SetActive(false);
            TutorialManager.m_instance.GoNextSequence();
            Debug.Log("Number of repetetion 2");


        }

        public void PlaySequence()
        {
            
            
            ShowCustomerDialogue();

            //m_triggerButton.onClick.AddListener(delegate { ChangeSequence(); });
            Debug.Log("Hello hello");

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

        public void PrintHello()
        {
            Debug.Log("Hello");
        }
    }
}

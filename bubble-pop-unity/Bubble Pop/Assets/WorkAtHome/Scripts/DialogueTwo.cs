using UnityEngine;
using TMPro;


namespace Assets.WorkAtHome.Scripts
{
    class DialogueTwo : MonoBehaviour, ISequence
    {
        [SerializeField] string m_dialogueText;
        [SerializeField] GameObject m_dialogueBox;

        private TMP_Text m_customerDialogueBoxText;
        public void ChangeSequence()
        {
            TutorialManager.m_instance.GoNextSequence();
        }

        public void PlaySequence()
        {
            ShowCustomerDialogue();
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

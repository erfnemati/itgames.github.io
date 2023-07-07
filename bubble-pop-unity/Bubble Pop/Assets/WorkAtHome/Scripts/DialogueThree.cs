using UnityEngine;
using TMPro;

namespace Assets.WorkAtHome.Scripts
{
    class DialogueThree : MonoBehaviour, ISequence
    {
        [SerializeField] string m_dialogueText;
        [SerializeField] GameObject m_dialogueBox;


        public void ChangeSequence()
        {
            TutorialManager.m_instance.GoNextSequence();
        }

        public void PlaySequence()
        {
            m_dialogueBox.SetActive(true);

            ShowCustomerDialogue();
        }

        public void ShowCustomerDialogue()
        {
           if (m_dialogueBox.GetComponentInChildren<TextMeshPro>()!= null)
            {
                m_dialogueBox.GetComponentInChildren<TextMeshPro>().text = m_dialogueText;
            }
        }
    }
}

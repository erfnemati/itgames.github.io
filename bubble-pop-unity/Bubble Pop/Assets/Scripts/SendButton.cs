using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Assets.Scripts
{
    public class SendButton : MonoBehaviour
    {
        [SerializeField] TMP_Text coinText;
        
        [SerializeField] GameObject m_oneHeartPacakge;
        [SerializeField] GameObject m_secHeartPackage;
        [SerializeField] GameObject m_thirdHeartPackage;


        public void SetButtonState(bool buttonState)
        {
            GetComponent<Button>().interactable = buttonState;
        }
        public void SendCustomer()
        {
            LevelManager.m_instance.SendProposal();
        }

        //Change ui button methods cause we have changed the name of button.
        public void SetButtonHearts()
        {
            int hearts = LevelManager.m_instance.GetLastCustomerHearts();
            if (hearts == 1)
            {
                m_oneHeartPacakge.SetActive(true);
            }
            else if (hearts == 2)
            {
                m_secHeartPackage.SetActive(true);
            }
            else if (hearts == 3)
            {
                m_thirdHeartPackage.SetActive(true);
            }
            Invoke(nameof(DeactivateHearts), 0.5f);
              
        }

        private void DeactivateHearts()
        {
            m_oneHeartPacakge.SetActive(false);
            m_secHeartPackage.SetActive(false);
            m_thirdHeartPackage.SetActive(false);
        }

    }
}

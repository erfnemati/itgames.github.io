using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


namespace Assets.Scripts
{
    public class SendButton : MonoBehaviour
    {
        public static SendButton m_instance;
        [SerializeField] TMP_Text coinText;
        [SerializeField] RectTransform m_sendButton;
        
        [SerializeField] GameObject m_oneHeartPacakge;
        [SerializeField] GameObject m_secHeartPackage;
        [SerializeField] GameObject m_thirdHeartPackage;
        [SerializeField] Sprite m_greenSendSprite;
        [SerializeField] Sprite m_purpleSendSprite;

        private void Start()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
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

        public void ShakeButton()
        {
            
            m_sendButton.DOShakeAnchorPos(0.5f,50f);
        }

        public void ChangeColor()
        {
            m_sendButton.GetComponent<Image>().sprite = m_greenSendSprite;
        }

        public void ResetButton()
        {
            m_sendButton.GetComponent<Image>().sprite = m_purpleSendSprite;
        }

    }
}

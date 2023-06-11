using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Assets.Scripts
{
    public class CustomerManager : MonoBehaviour
    {
        Request m_customerRequest;
        Proposal m_currentProposal = new Proposal();

        //UI variables here : 
        [SerializeField] TMP_Text m_requestText;
        [SerializeField] CustomerSlider m_customerSlider;
        void Start()
        {
            Invoke(nameof(SetRequest), 0.5f);
        }

        private void SetRequest()
        {
            m_customerRequest = RequestManager.m_instance.GetNewRequest();
            SetRequestText();
        }

        public void AddItem(Bubble bubble)
        {
            m_currentProposal.AddBubble(bubble);
            UpdateCustomerCompletionBar();
        }
        //UI code here : 

  
        private void SetRequestText()
        {
            m_requestText.text = "I need \n";
            if (m_customerRequest.GetRequestData().GetData() != 0)
            {
                m_requestText.text += m_customerRequest.GetRequestData().GetData() + "GB\n";
            }

            if (m_customerRequest.GetRequestCallTime().GetCallTime() != 0)
            {
                m_requestText.text += m_customerRequest.GetRequestCallTime().GetCallTime() + "Mins\n";
            }

            if (m_customerRequest.GetRequestMessage().GetMessageCount() != 0)
            {
                m_requestText.text += m_customerRequest.GetRequestMessage().GetMessageCount() + "SMS";
            }
        }

        private void UpdateCustomerCompletionBar()
        {

        }



    }
}

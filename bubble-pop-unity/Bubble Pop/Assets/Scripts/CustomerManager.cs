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
        [SerializeField] TMP_Text m_requestValue;
        [SerializeField] CustomerSlider m_customerSlider;
        void Start()
        {
            Invoke(nameof(SetRequest), 0.2f);
        }

        private void SetRequest()
        {
            m_customerRequest = RequestManager.m_instance.GetNewRequest();
            SetRequestText();
            SetRequestValueUi();
        }

        public void AddItem(Bubble bubble)
        {
            m_currentProposal.AddBubble(bubble);

            UpdateCustomerCompletionBar();
            SetRequestValueUi();
        }

        private float CheckProposal()
        {
            float dataPoint = 0;
            float callTimePoint = 0;
            float messagePoint = 0;
            int weight = 0;
            float requestData = m_customerRequest.GetRequestData().GetData();
            if(requestData >0)
            {
                weight++;
                float diffData = Mathf.Abs(m_currentProposal.GetProposalData().GetData() - requestData);
                if (diffData < Mathf.Epsilon)
                {
                    dataPoint = 1;
                }
                else
                {
                    dataPoint = 1 - diffData / requestData;
                    if (dataPoint < 0)
                    {
                        dataPoint = 0;
                    }
                }

            }

            float requestMessage = m_customerRequest.GetRequestMessage().GetMessageCount();
            if (requestMessage > 0)
            {
                weight++;
                float diffMessage = Mathf.Abs(m_currentProposal.GetProposalMessage().GetMessageCount() - requestMessage);
                if (diffMessage <= Mathf.Epsilon)
                {
                    messagePoint = 1;
                }
                else
                {
                    messagePoint = 1 - diffMessage / requestMessage;
                    if (messagePoint <0)
                    {
                        messagePoint = 0;
                    }
                }
                    
            }

            float requestCallTime = m_customerRequest.GetRequestCallTime().GetCallTime();
            if (requestCallTime > 0)
            {
                weight++;
                float diffCallTime = Mathf.Abs(m_currentProposal.GetProposalCallTime().GetCallTime() - requestCallTime);
                if (diffCallTime <= Mathf.Epsilon)
                {
                    callTimePoint = 1;
                }
                else
                {
                    callTimePoint = 1 - (diffCallTime / requestCallTime);
                    if (callTimePoint < 0)
                    {
                        callTimePoint = 0;
                    }
                }

            }
            float proposalPoint = (callTimePoint + messagePoint + dataPoint) / weight;
            return proposalPoint;



        }

        public int GetCoins()
        {
            float rateOfProposal = CheckProposal();
            int payedCoins = (int)(rateOfProposal * m_customerRequest.GetRequestValue());
            Debug.Log("Payed coins are : " + payedCoins);
            return payedCoins;
        }
        

  
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

        private void SetRequestValueUi()
        {
            m_requestValue.text =  m_customerRequest.GetRequestValue() + " Coins";
        }

        private void UpdateCustomerCompletionBar()
        {
            m_customerSlider.SetValue(CheckProposal());

        }



    }
}

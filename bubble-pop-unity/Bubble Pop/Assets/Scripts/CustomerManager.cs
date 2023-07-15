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
            if(requestData >=0)
            {
                if (requestData == 0)
                {
                    dataPoint = 0;
                }
                else
                {
                    weight++;
                    float diffData = m_currentProposal.GetProposalData().GetData() - requestData;
                    if (diffData > 0.0f)
                    {
                        dataPoint = 1;
                    }
                    else
                    {
                        dataPoint = 1 - Mathf.Abs(diffData / requestData);
                        if (dataPoint < 0)
                        {
                            dataPoint = 0;
                        }
                    }
                }

            }

            float requestMessage = m_customerRequest.GetRequestMessage().GetMessageCount();
            if (requestMessage >= 0)
            {
                if (requestMessage == 0)
                {
                    messagePoint = 0;
                }
                else
                {
                    weight++;
                    float diffMessage = m_currentProposal.GetProposalMessage().GetMessageCount() - requestMessage;
                    if (diffMessage >= 0.0f)
                    {
                        messagePoint = 1;
                    }
                    else
                    {
                        messagePoint = 1 - Mathf.Abs(diffMessage / requestMessage);
                        if (messagePoint < 0)
                        {
                            messagePoint = 0;
                        }
                    }
                }
                    
            }

            float requestCallTime = m_customerRequest.GetRequestCallTime().GetCallTime();
            if (requestCallTime >= 0)
            {
                if (requestCallTime == 0)
                {
                    callTimePoint = 0;
                }
                else
                {
                    weight++;
                    float diffCallTime = m_currentProposal.GetProposalCallTime().GetCallTime() - requestCallTime;
                    if (m_currentProposal.GetProposalCallTime().GetCallTime() == 0)
                    {
                        callTimePoint = 0;
                    }
                    else if (diffCallTime >= 0.0f)
                    {
                        callTimePoint = 1;
                    }
                    else
                    {
                        callTimePoint = 1 - (Mathf.Abs(diffCallTime) / requestCallTime);
                        if (callTimePoint < 0)
                        {
                            callTimePoint = 0;
                        }
                    }
                }

            }
            float proposalPoint = (callTimePoint + messagePoint + dataPoint) / weight;
            Debug.Log("Proposal point is : " + proposalPoint);
            return proposalPoint;
        }

        public int GetHearts()
        {
            float rateOfProposal = CheckProposal();
            float recievedHearts = rateOfProposal * m_customerRequest.GetRequestValue();
            int roundedHearts = 0;
            if (recievedHearts - 0.5f <= Mathf.Epsilon)
            {
                roundedHearts = 0;
            }

            else if (recievedHearts - 0.5f > Mathf.Epsilon && recievedHearts - 1.5f < Mathf.Epsilon)
            {
                roundedHearts = 1;
            }

            else if (recievedHearts - 1.5f >= Mathf.Epsilon && recievedHearts - 2.3 < Mathf.Epsilon)
            {
                roundedHearts = 2;
            }
            else
            {
                roundedHearts = 3;
            }
            Debug.Log("Final hearts : " + roundedHearts);
            return roundedHearts;
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

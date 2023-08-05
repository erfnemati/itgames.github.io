
using UnityEngine;

namespace Assets.Scripts
{
    public class CustomerManager : MonoBehaviour
    {
        Request m_customerRequest;
        Proposal m_currentProposal = new Proposal();

        [SerializeField] CustomerUiManager m_customerUiManager;

        private float m_lastValue = 0.0f;
        private float m_currentValue = 0.0f;

        void Start()
        {
            if (m_customerUiManager == null)
            {
                m_customerUiManager = GetComponent<CustomerUiManager>();
            }
            Invoke(nameof(SetRequest), 0.2f);
        }

        private void SetRequest()
        {
            SendButton.m_instance.ResetButton();
            m_customerRequest = RequestManager.m_instance.GetNewRequest();
            m_customerUiManager.SetRequestText(m_customerRequest);
        }

        public void AddItem(Bubble bubble)
        {
            m_currentProposal.AddBubble(bubble);
            Vector3 position = bubble.transform.position;

            m_lastValue = m_currentValue;
            m_currentValue = CheckProposal();
            m_customerUiManager.InstantiatePopedIcon(position,m_currentValue,m_lastValue);

        }

        //This function check how good is the player proposal
        //Ranges between 0 to 1
        private float CheckProposal()
        {
            if (m_customerRequest == null)
            {
                return 0;
            }

            float dataPoint = 0;
            float callTimePoint = 0;
            float messagePoint = 0;
            int weight = 0;

            GetProposalDataPoint(ref dataPoint, ref weight);
       
            GetProposalMessagePoint(ref messagePoint, ref weight);
           
            GetProposalCallTimePoint(ref callTimePoint, ref weight);
           

            float proposalPoint = (callTimePoint + messagePoint + dataPoint) / weight;
            if(proposalPoint <= Mathf.Epsilon)
            {
                proposalPoint = 0;
            }
            return proposalPoint;
        }

        private void GetProposalCallTimePoint(ref float callTimePoint, ref int weight)
        {
            float requestCallTime = m_customerRequest.GetRequestCallTime().GetCallTime();
            if (m_currentProposal.GetProposalCallTime().GetCallTime() != 0 || requestCallTime != 0)
            {
                weight++;
                float diffCallTime = Mathf.Abs(m_currentProposal.GetProposalCallTime().GetCallTime() - requestCallTime);

                if (diffCallTime < Mathf.Epsilon)
                {
                    callTimePoint = 1;
                }
                else
                {
                    callTimePoint = 1 - (Mathf.Abs(diffCallTime) / requestCallTime);
                    if (callTimePoint  <= Mathf.Epsilon)
                    {
                        callTimePoint = 0f;
                    }
                }
            }
        }

        private void GetProposalMessagePoint(ref float messagePoint, ref int weight)
        {
            float requestMessage = m_customerRequest.GetRequestMessage().GetMessageCount();
            if (m_currentProposal.GetProposalMessage().GetMessageCount() != 0 || requestMessage != 0)
            {
                weight++;
                float diffMessage = Mathf.Abs(m_currentProposal.GetProposalMessage().GetMessageCount() - requestMessage);
                if (diffMessage < Mathf.Epsilon)
                {
                    messagePoint = 1;
                }
                else
                {
                    messagePoint = 1 - Mathf.Abs(diffMessage / requestMessage);
                    if (messagePoint  <= Mathf.Epsilon)
                    {
                        messagePoint = 0f;
                    }
                }
            }
        }

        private void GetProposalDataPoint(ref float dataPoint, ref int weight)
        {
            float requestData = m_customerRequest.GetRequestData().GetData();
            if (m_currentProposal.GetProposalData().GetData() != 0 || requestData != 0)
            {
                weight++;
                float diffData = Mathf.Abs(m_currentProposal.GetProposalData().GetData() - requestData);

                if (diffData < Mathf.Epsilon)
                {
                    dataPoint = 1;
                }
                else
                {

                    dataPoint = 1 - Mathf.Abs(diffData / requestData);
                    if (dataPoint  <= Mathf.Epsilon)
                    {
                        dataPoint = 0f;
                    }
                }
            }
        }

        public int GetHearts()
        {
            if (m_customerRequest == null)
            {
                return 0;
            }
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
            return roundedHearts;
        }
    }
}

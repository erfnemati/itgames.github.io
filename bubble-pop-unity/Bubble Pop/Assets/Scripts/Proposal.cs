using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    class Proposal:MonoBehaviour
    {
        Data m_data;
        CallTime m_callTime;
        Message m_messages;

        List<Bubble> m_bubbleList = new List<Bubble>();

        

        [SerializeField]TMP_Text m_proposalText;

        

        public Proposal()
        {
            m_data = new Data(0);
            m_callTime = new CallTime(0);
            m_messages = new Message(0);
        }

        private void Start()
        {
            m_proposalText.text = "Empty";
        }

        public void AddBubble(Bubble bubble)
        {
            m_bubbleList.Add(bubble);

            if (bubble.getBubbleData() != null)
            {
                m_data.Add(bubble.getBubbleData().GetData());
            }

            if (bubble.GetBubbleCallTime() != null)
            {
                m_callTime.Add(bubble.GetBubbleCallTime().getCallTime());
            }

            if (bubble.GetBubbleMessage() !=  null)
            {
                m_messages.Add(bubble.GetBubbleMessage().GetMessageCount());

            }

            UpdateProposalText();

        }

        //UI code here : 

        public void UpdateProposalText()
        {
            m_proposalText.text = null;
            if (m_data.GetData() != 0)
            {
                string dataText = m_data.GetData() + "GB\n";
                m_proposalText.text += dataText;
            }

            if (m_callTime.getCallTime() != 0)
            {
                string callTimeText = m_callTime.getCallTime() + "Mins\n";
                m_proposalText.text += callTimeText;
            }

            if (m_messages.GetMessageCount() != 0)
            {
                string messageText = m_messages.GetMessageCount() + "SMS";
                m_proposalText.text += messageText;
            }
            
        }
    }
}

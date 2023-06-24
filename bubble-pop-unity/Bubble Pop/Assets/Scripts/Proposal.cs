using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    class Proposal
    {
        Data m_data;
        CallTime m_callTime;
        Message m_messages;


        public Proposal()
        {
            m_data = new Data(0);
            m_callTime = new CallTime(0);
            m_messages = new Message(0);
        }

        public void AddBubble(Bubble bubble)
        {

            if (bubble.GetBubbleData() != null)
            {
                m_data.Add(bubble.GetBubbleData().GetData());
                
            }

            if (bubble.GetBubbleCallTime() != null)
            {
                m_callTime.Add(bubble.GetBubbleCallTime().GetCallTime());
                //Debug.Log("proposal call time is " + m_callTime.GetCallTime());
                
            }

            if (bubble.GetBubbleMessage() !=  null)
            {
                m_messages.Add(bubble.GetBubbleMessage().GetMessageCount());
                

            }
        }

        public void Clear()
        {
            //Debug.Log("No need for clear");
        }

        public Data GetProposalData()
        {
            return m_data;
        }

        public CallTime GetProposalCallTime()
        {
            return m_callTime;
        }

        public Message GetProposalMessage()
        {
            return m_messages;
        }
    }
}

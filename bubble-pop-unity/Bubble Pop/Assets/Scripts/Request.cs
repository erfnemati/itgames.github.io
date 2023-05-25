using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assets.Scripts
{
    public class Request
    {
        Data m_neededData;
        CallTime m_neededCallTime;
        Message m_neededMessage;

        public Request()
        {
            m_neededData = new Data();
            m_neededCallTime = new CallTime();
            m_neededMessage = new Message();
        }

        public Request(Data data =null , CallTime callTime = null , Message message = null)
        {
            if (data == null)
            {
                m_neededData = new Data();
            }
            else
            {
                m_neededData = data;
            }

            if (callTime == null)
            {
                m_neededCallTime = new CallTime();
            }
            else
            {
                m_neededCallTime = callTime;
            }

            if (message == null)
            {
                m_neededMessage = new Message();
            }
            else
            {
                m_neededMessage = message;
            }
        }

        public Data GetRequestData()
        {
            return m_neededData;
        }

        public CallTime GetRequestCallTime()
        {
            return m_neededCallTime;
        }

        public Message GetRequestMessage()
        {
            return m_neededMessage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PackageContent
    {

        //TODO : We can change member fields so all of them inherit from an content interface
        private Data m_data;//Unit is GB
        private CallTime m_callTime;//Unit is Minute
        private Message m_messages;//Unit is integers
        private int m_numOfContents;

        public PackageContent(Data packageData = null, CallTime packageCallTime = null, Message packageMessagesCount = null)
        {
            m_data = packageData;
            m_callTime = packageCallTime;
            m_messages = packageMessagesCount;
            SetNumOfContents();
            
        }

        public PackageContent()
        {
            System.Random rand = new System.Random();
            int randomContentType = rand.Next(1, 4);
            if (randomContentType == 1)
            {
                m_data = new Data();
                m_callTime = null;
                m_messages = null;
            }
            else if (randomContentType == 2)
            {
                m_data = null;
                m_callTime = new CallTime();
                m_messages = null;

            }
            else if (randomContentType == 3)
            {
                m_data = null;
                m_callTime = null;
                m_messages = new Message();

            }

            SetNumOfContents();
        }

        public string GetPackageTextContent()
        {
            string dataText;
            string callTimeText;
            string messagesCount;
            string packageContent;

            if (m_data == null)
            {
                dataText = null;
            }
            else
            {
                dataText = m_data.GetData() + "GB\n";
            }
            
            if (m_callTime == null)
            {
                callTimeText = null;
            }
            else
            {
                callTimeText = m_callTime.getCallTime() + "Mins\n";
            }
            
            if (m_messages == null)
            {
                messagesCount = null;
            }
            else
            {
                messagesCount = m_messages.GetMessageCount() + "SMS";
            }
           
            packageContent = dataText + callTimeText + messagesCount;
            return packageContent;

        }

        private void SetNumOfContents()
        {
            if (m_data != null)
            {
                m_numOfContents++;
            }

            if (m_callTime != null)
            {
                m_numOfContents++;
            }

            if (m_messages != null)
            {
                m_numOfContents++;
            }
        }

        public int GetNumOfContents()
        {
            return m_numOfContents;
        }

        public Data GetDataContent()
        {
            return m_data;
        }

        public CallTime GetCallTime()
        {
            return m_callTime;
        }

        public Message GetMessage()
        {
            return m_messages;
        }

    }

}

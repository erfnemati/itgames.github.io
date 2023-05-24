using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Message : Content
    {
        private int m_numOfMessages;

        public Message()
        {
            System.Random rand = new System.Random();
            m_numOfMessages = rand.Next(100, 1000);

            int integerPart = 0;
            integerPart = m_numOfMessages / 100;
            m_numOfMessages = integerPart * 100;
            Debug.Log("Messages is " + m_numOfMessages);

            

        }

        public Message(int numOfMessage)
        {
            m_numOfMessages = numOfMessage;
        }
        
        public int GetMessageCount()
        {
            return m_numOfMessages;
        }
    }
}

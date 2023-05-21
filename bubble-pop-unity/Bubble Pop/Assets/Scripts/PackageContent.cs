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
        private int m_data;//Unit is GB
        private int m_callTime;//Unit is Minute
        private int m_messages;//Unit is integers

        public PackageContent(int packageData = 0 ,int packageCallTime = 0,  int packageMessagesCount = 0)
        {
            m_data = packageData;
            m_callTime = packageCallTime;
            m_messages = packageMessagesCount;
        }

        public PackageContent()
        {
            System.Random rand = new System.Random();
            int integerPart = 0;

            m_data = rand.Next(1, 100);
            if (m_data - 10 > float.Epsilon)
            {
                integerPart = m_data / 10;
                m_data = integerPart * 10;
            }
            
            m_callTime = rand.Next(10, 1000);
            if (m_callTime - 100 < float.Epsilon)
            {
                integerPart = m_data / 10;
                m_callTime = integerPart * 10;
            }
            else
            {
                integerPart = m_callTime / 100;
                m_callTime = integerPart * 100;
            }
            

            m_messages = rand.Next(100, 1000);
            integerPart = m_messages / 100;
            m_messages = (int)integerPart * 100;
        }

        

        public float getPackageData()
        {
            return m_data;
        }

        public float getPackageCallTime()
        {
            return m_callTime;
        }

        public int getPackageMessages()
        {
            return m_messages;
        }
    }


    
}

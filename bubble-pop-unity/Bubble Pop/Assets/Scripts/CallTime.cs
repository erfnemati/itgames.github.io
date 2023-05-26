using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CallTime : Content
    {
        int m_callTime;

        public CallTime()
        {
            System.Random rand = new System.Random();
            int integerPart = 0;



            m_callTime = rand.Next(10, 1000);
            if (m_callTime - 100 < float.Epsilon)
            {
                integerPart = m_callTime / 10;
                m_callTime = integerPart * 10;
            }
            else
            {
                integerPart = m_callTime / 100;
                m_callTime = integerPart * 100;
            }
        }

        public CallTime(int callTime = 0)
        {
            m_callTime = callTime;
        }

        public int GetCallTime()
        {
            return m_callTime;
        }

        public override void Add(int number)
        {
            m_callTime += number;
        }
    }
}

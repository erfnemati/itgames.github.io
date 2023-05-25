using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Data : Content
    {
        int m_data;
      
        public Data()
        {
            System.Random rand = new System.Random();
            int integerPart = 0;

            m_data = rand.Next(1, 100);
            if (m_data - 10 > float.Epsilon)
            {
                integerPart = m_data / 10;
                m_data = integerPart * 10;
            }
            Debug.Log("Data is " + m_data);
            
        }

        public Data (int data = 0)
        {
            m_data = data;
        }

        public int GetData()
        {
            return m_data;
        }

        public override void Add(int number)
        {
            m_data += number;
        }
    }
}

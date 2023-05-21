using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class PackageContent
    {
        private float m_data;//Unit is GB
        private float m_callTime;//Unit is Minute
        private int m_messages;//Unit is integers

        PackageContent(float packageData = 0 ,float packageCallTime = 0 ,  int packageMessagesCount = 0)
        {
            m_data = packageData;
            m_callTime = packageCallTime;
            m_messages = packageMessagesCount;
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

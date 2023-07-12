using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class RequestManager:MonoBehaviour
    {
        List<Data> m_dataList = new List<Data>();
        List<CallTime> m_callTimeList = new List<CallTime>();
        List<Message> m_messageList = new List<Message>();

        public static RequestManager m_instance;

        private int m_numberOfTries = 3;
        
      

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
           
        }

        public Request GetNewRequest()
        {
            int requestValue = 0;
            Debug.Log("Getting a new customer!");
            Data requestData = new Data(0);
            CallTime requestCallTime = new CallTime(0);
            Message requestMessage = new Message(0);



            bool isPicked = false;
            System.Random rand = new System.Random();
            float tempNumber;
            Data tempData;
            CallTime tempCallTime;
            Message tempMessage;

            for (int i = 0; i < m_numberOfTries; i++)
            {
               



                if (rand.Next(0, 5) == 0 && isPicked)
                {
                    tempNumber = (rand.Next(7, 13) / 10f) * (requestData.GetData());
                    tempNumber = (((int)tempNumber / 10) * 10);
                    tempData = new Data((int)tempNumber);

                    tempNumber = (rand.Next(7, 13) / 10f) * (requestCallTime.GetCallTime());
                    tempNumber = (((int)tempNumber / 10) * 10);
                    tempCallTime = new CallTime((int)tempNumber);

                    tempNumber = (rand.Next(7, 13) / 10f) * (requestMessage.GetMessageCount());
                    tempNumber = (((int)tempNumber / 10) * 10);
                    tempMessage = new Message((int)tempNumber);

                    requestValue = SetRequestValue(tempData, tempCallTime, tempMessage);
                    return new Request(tempData, tempCallTime, tempMessage, requestValue);

                }


                int chosenContent = rand.Next(1, 4);
                switch (chosenContent)
                {
                    case 1:
                        if (m_dataList.Count == 0)
                            break;
                        int dataIndex = rand.Next(0, m_dataList.Count);
                        requestData.Add(m_dataList[dataIndex].GetData());
                        isPicked = true;
                        m_dataList.RemoveAt(dataIndex);
                        break;

                    case 2:
                        if (m_callTimeList.Count == 0)
                            break;
                        int callTimeIndex = rand.Next(0, m_callTimeList.Count);
                        requestCallTime.Add(m_callTimeList[callTimeIndex].GetCallTime());
                        isPicked = true;
                        m_callTimeList.RemoveAt(callTimeIndex);
                        break;

                    case 3:
                        if (m_messageList.Count == 0)
                            break;
                        int messageIndex = rand.Next(0, m_messageList.Count);
                        requestMessage.Add(m_messageList[messageIndex].GetMessageCount());
                        isPicked = true;
                        m_messageList.RemoveAt(messageIndex);
                        break;
                }
            }

            tempNumber = (rand.Next(7, 13) / 10f) * (requestData.GetData());
            tempNumber = (((int)tempNumber / 10) * 10);
            tempData = new Data((int)tempNumber);

            tempNumber = (rand.Next(7, 13) / 10f) * (requestCallTime.GetCallTime());
            tempNumber = (((int)tempNumber / 10) * 10);
            tempCallTime = new CallTime((int)tempNumber);

            tempNumber = (rand.Next(7, 13) / 10f) * (requestMessage.GetMessageCount());
            tempNumber = (((int)tempNumber / 10) * 10);
            tempMessage = new Message((int)tempNumber);

            requestValue = SetRequestValue(tempData, tempCallTime, tempMessage);
            return new Request(tempData, tempCallTime, tempMessage, requestValue);
        }

        private  int SetRequestValue(Data requestData, CallTime requestCallTime, Message requestMessage)
        {
            int requestValue = 0;
            int multiPlier = 0;
            if (requestData.GetData() != 0)
            {
                requestValue += 50;
                multiPlier++;
            }

            if (requestCallTime.GetCallTime() != 0)
            {
                requestValue += 50;
                multiPlier++;
            }

            if (requestMessage.GetMessageCount() != 0)
            {
                requestValue += 50;
                multiPlier++;
            }

            requestValue = requestValue + (multiPlier * requestValue);
            Debug.Log("Request value is : " + requestValue);
            return requestValue;
        }

        public void RefreshLists()
        {
            m_dataList.Clear();
            m_callTimeList.Clear();
            m_messageList.Clear();

            foreach (Data temp in LevelManager.m_instance.GetDataList())
            {
                m_dataList.Add(temp);
            }

            foreach (CallTime temp in LevelManager.m_instance.GetCallTimeList())
            {
                m_callTimeList.Add(temp);
            }

            foreach (Message temp in LevelManager.m_instance.GetMessageList())
            {
                m_messageList.Add(temp);
            }

        }
    }
}

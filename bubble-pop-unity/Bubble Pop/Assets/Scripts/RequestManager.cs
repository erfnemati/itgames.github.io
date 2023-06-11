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
        
            Request newRequest = null;
            Data requestData =new Data(0) ;
            CallTime requestCallTime = new CallTime(0);
            Message requestMessage = new Message(0);

            

            bool isPicked = false;
            for (int i = 0; i < m_numberOfTries; i++)
            {
                System.Random rand = new System.Random();
                
                if (rand.Next(0,5) == 0 && isPicked)
                {
                    newRequest = new Request(requestData, requestCallTime, requestMessage);
                    return newRequest;
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
            Debug.Log("New data  request is : " + requestData.GetData());
            return new Request(requestData, requestCallTime, requestMessage);
        }

        public void RefreshLists()
        {
            Debug.Log("Lists have been refreshed");
            foreach (Data temp in LevelManager.m_instance.GetDataList())
            {
                m_dataList.Add(temp);
                Debug.Log("newly added data : " + temp.GetData());
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

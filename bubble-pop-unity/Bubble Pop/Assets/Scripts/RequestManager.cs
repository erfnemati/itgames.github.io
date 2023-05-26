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
            //Three different list cloned from level manager.
            List<Data> dataList = new List<Data>();
            List<CallTime> callTimeList = new List<CallTime>();
            List<Message> messageList = new List<Message>();

            Request newRequest = null;
            Data requestData =new Data(0) ;
            CallTime requestCallTime = new CallTime(0);
            Message requestMessage = new Message(0);

            foreach (Data temp in LevelManager.m_instance.GetDataList())
            {
                dataList.Add(temp);
            }

            foreach(CallTime temp in LevelManager.m_instance.GetCallTimeList())
            {
                callTimeList.Add(temp);
            }

            foreach(Message temp in LevelManager.m_instance.GetMessageList())
            {
                messageList.Add(temp);
            }

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
                        if (dataList.Count == 0)
                            break;
                        int dataIndex = rand.Next(0, dataList.Count);
                        requestData.Add(dataList[dataIndex].GetData());
                        isPicked = true;
                        dataList.RemoveAt(dataIndex);
                        break;

                    case 2:
                        if (callTimeList.Count == 0)
                            break;
                        int callTimeIndex = rand.Next(0, callTimeList.Count);
                        requestCallTime.Add(callTimeList[callTimeIndex].GetCallTime());
                        isPicked = true;
                        callTimeList.RemoveAt(callTimeIndex);
                        break;

                    case 3:
                        if (messageList.Count == 0)
                            break;
                        int messageIndex = rand.Next(0, messageList.Count);
                        requestMessage.Add(messageList[messageIndex].GetMessageCount());
                        isPicked = true;
                        messageList.RemoveAt(messageIndex);
                        break;
                }
            }
            return new Request(requestData, requestCallTime, requestMessage);
        }
    }
}

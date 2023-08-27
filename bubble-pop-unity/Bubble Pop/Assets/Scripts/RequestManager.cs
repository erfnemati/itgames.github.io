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

        [SerializeField] int m_numberOfTries = 3;

        [SerializeField] RequestGenerationModel m_model = RequestGenerationModel.Addition;
        
      

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
            if (m_model == RequestGenerationModel.Addition)
            {
                return GetNewExactRequest();
            }
            else
            {
                return GetNewApproximateRequest();
            }
        }

        private Request GetNewExactRequest()
        {
            int requestValue = 0;
            Data requestData = new Data(0);
            CallTime requestCallTime = new CallTime(0);
            Message requestMessage = new Message(0);

            System.Random rand = new System.Random();
            int m_numOfAdditions = rand.Next(1, m_numberOfTries);
            for (int i = 0; i < m_numOfAdditions;i++)
            {
                int typeOfContent = rand.Next(1, 3);
                if (typeOfContent == 1 && m_dataList.Count == 0)
                {
                    typeOfContent = 2;
                }

                if (typeOfContent == 2 && m_callTimeList.Count == 0)
                {
                    typeOfContent = 3;
                }

                if (typeOfContent == 3 && m_messageList.Count == 0)
                {
                    typeOfContent = 1;
                }

                int index = 0;
                if (typeOfContent == 1)
                {
                    index = rand.Next(0, m_dataList.Count);
                    requestData.Add(m_dataList[index].GetData());
                    m_dataList.RemoveAt(index);
                }
                else if (typeOfContent == 2)
                {
                    index = rand.Next(0, m_callTimeList.Count);
                    requestCallTime.Add(m_callTimeList[index].GetCallTime());
                    m_callTimeList.RemoveAt(index);
                  
                }

                else if (typeOfContent == 3)
                {
                    index = rand.Next(0, m_messageList.Count);
                    requestMessage.Add(m_messageList[index].GetMessageCount());
                    m_messageList.RemoveAt(index);
                }

            }
            requestValue = SetRequestValue(requestData, requestCallTime, requestMessage);
            return new Request(requestData, requestCallTime, requestMessage, requestValue);
        }

        private Request GetNewApproximateRequest()
        {
            int requestValue = 0;
            Data requestData = new Data(0);
            CallTime requestCallTime = new CallTime(0);
            Message requestMessage = new Message(0);

            System.Random rand = new System.Random();
            int m_numOfAdditions = rand.Next(1, 5);
            for (int i = 0; i < m_numOfAdditions; i++)
            {
                int typeOfContent = rand.Next(1, 3);
                if (typeOfContent == 1 && m_dataList.Count == 0)
                {
                    typeOfContent = 2;
                }

                if (typeOfContent == 2 && m_callTimeList.Count == 0)
                {
                    typeOfContent = 3;
                }

                if (typeOfContent == 3 && m_messageList.Count == 0)
                {
                    typeOfContent = 1;
                }

                int index = 0;
                if (typeOfContent == 1)
                {
                    index = rand.Next(0, m_dataList.Count);
                    requestData.Add(m_dataList[index].GetData());
                    m_dataList.RemoveAt(index);
                }
                else if (typeOfContent == 2)
                {
                    index = rand.Next(0, m_callTimeList.Count);
                    requestCallTime.Add(m_callTimeList[index].GetCallTime());
                    m_callTimeList.RemoveAt(index);

                }

                else if (typeOfContent == 3)
                {
                    index = rand.Next(0, m_messageList.Count);
                    requestMessage.Add(m_messageList[index].GetMessageCount());
                    m_messageList.RemoveAt(index);
                }

            }

            float tempNumber = 0.0f;
            Data tempData = null;
            CallTime tempCallTime = null;
            Message tempMessage = null;

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
            return 3;
            /*int requestValue = 0;
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
            return requestValue;*/
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

    public enum RequestGenerationModel
    {
        Addition , Approximitation
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager m_instance;

        [SerializeField] TMP_Text m_requestText;
        [SerializeField] TMP_Text m_proposalText;

        [SerializeField] int m_numOfBubbles = 5;
        [SerializeField] GameObject m_bubblePrefab;
        [SerializeField] Transform[] m_bubbleTransfroms;

        public List<Data> m_dataList = new List<Data>();
        public List<CallTime> m_callTimeList = new List<CallTime>();
        public List<Message> m_messageList = new List<Message>();


        Request m_currentRequest;
        Proposal m_proposal;
        void Start()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            GenerateBubbles(m_numOfBubbles);
            SetRequest();
            m_proposal = FindObjectOfType<Proposal>();
        }

        public void AddItem(Bubble bubble)
        {
            m_proposal.AddBubble(bubble);
        }

        private void SetRequest()
        {
            m_currentRequest = RequestManager.m_instance.GetNewRequest();
            SetRequestText();
        }

        private void GenerateBubbles(int numberOfBubbles)
        {
            for(int i = 0; i < numberOfBubbles; i++)
            {
                
                GameObject instantiatedOne = Instantiate(m_bubblePrefab, m_bubbleTransfroms[i].position,Quaternion.identity);
                
                Bubble bubbleScript = instantiatedOne.GetComponent<Bubble>();
                
                if (bubbleScript.GetBubbleData() != null )
                {
                    m_dataList.Add(bubbleScript.GetBubbleData());
                }

                if (bubbleScript.GetBubbleCallTime() != null)
                {
                    m_callTimeList.Add(bubbleScript.GetBubbleCallTime());
                }

                if (bubbleScript.GetBubbleMessage() != null)
                {
                    m_messageList.Add(bubbleScript.GetBubbleMessage());
                }
                
            }
        }

        private void SetRequestText()
        {
            m_requestText.text = "I need \n";
            if (m_currentRequest.GetRequestData().GetData() != 0)
            {
                m_requestText.text += m_currentRequest.GetRequestData().GetData() + "GB\n";
            }

            if (m_currentRequest.GetRequestCallTime().GetCallTime() != 0)
            {
                m_requestText.text += m_currentRequest.GetRequestCallTime().GetCallTime() + "Mins\n";
            }

            if (m_currentRequest.GetRequestMessage().GetMessageCount() != 0)
            {
                m_requestText.text += m_currentRequest.GetRequestMessage().GetMessageCount() + "SMS";
            }
        }

        public List<Data> GetDataList()
        {
            return m_dataList;
        }

        public List<CallTime> GetCallTimeList()
        {
            return m_callTimeList;
        }

        public List<Message> GetMessageList()
        {
            return m_messageList;
        }

        public void SendProposal()
        {
            //TODO
        }

        public void DiscardProposal()
        {
            //TODO
        }



    }
}

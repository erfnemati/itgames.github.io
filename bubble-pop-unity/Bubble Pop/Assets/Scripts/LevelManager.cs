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

        [SerializeField] int m_numOfBubbles = 2;
        [SerializeField] GameObject m_bubblePrefab;
        [SerializeField] Transform[] m_bubbleTransfroms;

        private List<Data> m_dataList = new List<Data>();
        private List<CallTime> m_callTimeList = new List<CallTime>();
        private List<Message> m_messageList = new List<Message>();

        private List<Bubble> m_chosenBubbles = new List<Bubble>();
        private List<Transform> vacantTransforms = new List<Transform>();


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
            SetCurrentRequest();
            m_proposal = FindObjectOfType<Proposal>();
        }

        public void AddItem(Bubble bubble)
        {
            m_chosenBubbles.Add(bubble);
            m_proposal.AddBubble(bubble);
        }

        public void SendProposal()
        {
            if (IsRightProposalSent())
            {
                Debug.Log("You are right");
                foreach(Bubble temp in m_chosenBubbles)
                {
                    GameObject newBubble = Instantiate(m_bubblePrefab, temp.transform.position, Quaternion.identity);
                    CashBubbleInfo(newBubble);

                    RemoveBubbleInfo(temp.GetComponent<Bubble>());
                    Destroy(temp.gameObject);
                }
                m_proposal.Clear();
                m_chosenBubbles.Clear();
                SetCurrentRequest();
            }
            else
            {
                Debug.Log("You are wrong");
                DiscardProposal();
            }
        }

        public void RemoveBubbleInfo(Bubble bubble)
        {
            if (bubble.GetBubbleData() != null)
            {
                m_dataList.Remove(bubble.GetBubbleData());
            }

            if (bubble.GetBubbleCallTime() != null)
            {
                m_callTimeList.Remove(bubble.GetBubbleCallTime());
            }

            if (bubble.GetBubbleMessage() != null)
            {
                m_messageList.Remove(bubble.GetBubbleMessage());
            }
        }

        public void DiscardProposal()
        {
            foreach(Bubble temp in m_chosenBubbles)
            {
                temp.gameObject.SetActive(true);
            }
            m_chosenBubbles.Clear();
            m_proposal.Clear();
        }

        private bool IsRightProposalSent()
        {
            if (m_currentRequest.GetRequestData() != null)
            {
                if (m_currentRequest.GetRequestData().GetData() != m_proposal.GetProposalData().GetData())
                {
                    return false;
                }
            }
            if (m_currentRequest.GetRequestCallTime() != null)
            {
                if (m_currentRequest.GetRequestCallTime().GetCallTime() != m_proposal.GetProposalCallTime().GetCallTime())
                {
                    return false;
                }
            }
            if (m_currentRequest.GetRequestMessage() != null)
            {
                if (m_currentRequest.GetRequestMessage().GetMessageCount() != m_proposal.GetProposalMessage().GetMessageCount())
                {
                    return false;
                }
            }

            return true;
        }

        private void SetCurrentRequest()
        {
            m_currentRequest = RequestManager.m_instance.GetNewRequest();
            SetRequestText();
        }

        private void GenerateBubbles(int numberOfBubbles)
        {
            for(int i = 0; i < numberOfBubbles; i++)
            {

                GameObject instantiatedOne = Instantiate(m_bubblePrefab, m_bubbleTransfroms[i].position, Quaternion.identity);

                CashBubbleInfo(instantiatedOne);

            }
        }

        private void CashBubbleInfo(GameObject instantiatedOne)
        {
            Bubble bubbleScript = instantiatedOne.GetComponent<Bubble>();

            if (bubbleScript.GetBubbleData() != null)
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
    }
}

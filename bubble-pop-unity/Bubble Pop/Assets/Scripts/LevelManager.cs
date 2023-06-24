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

        private List<Data> m_dataList = new List<Data>();
        private List<CallTime> m_callTimeList = new List<CallTime>();
        private List<Message> m_messageList = new List<Message>();

        private List<Bubble> m_chosenBubbles = new List<Bubble>();
        private Queue<Vector3> vacantTransforms = new Queue<Vector3>();

        private int m_numOfActiveBubbles = 0;

        [SerializeField] CustomerManager m_customer;
        [SerializeField] GameObject m_customerGameObject;


        Request m_currentRequest;
        Proposal m_proposal;
        void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            
            
            //SetCurrentRequest();
            //m_proposal = FindObjectOfType<Proposal>();
        }

        public void Start()
        {
            AddVacantInitialTransforms();
            GenerateBubbles(m_numOfBubbles - m_numOfActiveBubbles);
            InstantiateNewCustomer();
            
            
        }

        public void GetNewCustomer()
        {
            GenerateBubbles(m_numOfBubbles - m_numOfActiveBubbles);
            InstantiateNewCustomer();
        }

        private void InstantiateNewCustomer()
        {
            RequestManager.m_instance.RefreshLists();
            if (m_customer != null)
            {
                Destroy(m_customer.gameObject);
            }
            GameObject customer = Instantiate(m_customerGameObject);
            SetCurrentCustomer(customer.GetComponent<CustomerManager>());
        }


        public void SetCurrentCustomer(CustomerManager customer)
        {
            m_customer = customer;
        }

        private void AddVacantTransform(Bubble bubble)
        {
            Vector3 bubblePosition = bubble.transform.position;
            Vector3 temp = new Vector3(bubblePosition.x,bubblePosition.y,bubblePosition.z);
            vacantTransforms.Enqueue(temp);
        }

        private void AddVacantInitialTransforms()
        {
            foreach (Transform temp in m_bubbleTransfroms)
            {
                vacantTransforms.Enqueue(temp.position);
            }
        }


        public void AddItem(Bubble bubble)
        {
            
            //m_chosenBubbles.Add(bubble);
            m_numOfActiveBubbles--;
            m_customer.AddItem(bubble);
            AddVacantTransform(bubble);
            RemoveBubbleInfo(bubble);
            Destroy(bubble.gameObject);
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
            m_numOfActiveBubbles += numberOfBubbles;
            //Debug.Log("Number of to be generated bubbles " + numberOfBubbles);
            //Debug.Log("Queue count is " + vacantTransforms.Count);
            for(int i = numberOfBubbles - 1; i >= 0; i--)
            {
                Debug.Log("index is " + i);
                GameObject instantiatedOne = Instantiate(m_bubblePrefab, vacantTransforms.Dequeue(), Quaternion.identity);

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

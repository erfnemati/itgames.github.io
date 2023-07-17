using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager m_instance;

        [SerializeField] int m_numOfBubbles = 5;
        [SerializeField] GameObject m_bubblePrefab;
        [SerializeField] Transform[] m_bubbleTransfroms;

        private List<Data> m_dataList = new List<Data>();
        private List<CallTime> m_callTimeList = new List<CallTime>();
        private List<Message> m_messageList = new List<Message>();
        private List<Transform> m_generatedBubbles = new List<Transform>();

        private Queue<Vector3> vacantTransforms = new Queue<Vector3>();

        private int m_numOfActiveBubbles = 0;

        CustomerManager m_customer;
        [SerializeField] GameObject m_customerGameObject;

        private float m_lastCustomerEarnedHearts = 0.0f;
        private float m_madeHearts = 0f;
        private int m_numOfAnsweredCustomers = 0;
        private bool m_isLevelFinished = false;
        private int m_numOfPopedBubbles = 0;



        [SerializeField] int m_mainMenuBuildIndex = 0;


        [SerializeField] Button m_sendButton;
        void Awake()
        {
            if(m_instance != null && m_instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                m_instance = this;
            }
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
            m_numOfAnsweredCustomers++;
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
            m_numOfPopedBubbles++;
            m_numOfActiveBubbles--;
            m_customer.AddItem(bubble);
            AddVacantTransform(bubble);
            RemoveBubbleInfo(bubble);
            Destroy(bubble.gameObject);
        }

        public void SendProposal()
        {
            if (m_customer == null)
            {
                return;
            }
            m_lastCustomerEarnedHearts = m_customer.GetHearts();
            m_madeHearts += m_lastCustomerEarnedHearts;
            MoneyLimitedCustomer.m_instance.UpdateRecievedHearts((int)m_madeHearts);
            m_numOfAnsweredCustomers++;
            GetNewCustomer();
        }

        public int GetLastCustomerHearts()
        {
            int hearts = (int)m_lastCustomerEarnedHearts;
            return hearts;
           
        }



        public void RemoveBubbleInfo(Bubble bubble)
        {
            m_generatedBubbles.Remove(bubble.transform);
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

        private void GenerateBubbles(int numberOfBubbles)
        {
            m_numOfActiveBubbles += numberOfBubbles;
            for(int i = numberOfBubbles - 1; i >= 0; i--)
            {
                GameObject instantiatedOne = Instantiate(m_bubblePrefab, vacantTransforms.Dequeue(), Quaternion.identity);
                m_generatedBubbles.Add(instantiatedOne.transform);

                CashBubbleInfo(instantiatedOne);
            }
            PlaceBubbles();

        }

        private void PlaceBubbles()
        {
            for (int i = 0; i < m_numOfActiveBubbles; i++)
            {
                m_generatedBubbles[i].transform.position = m_bubbleTransfroms[i].position;
                m_generatedBubbles[i].GetComponent<Bubble>().SetInitialPos();
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

        public int GetNumberOfAnsweredCustomers()
        {
            return m_numOfAnsweredCustomers;
        }
        public float GetRecievedHearts()
        {
            return m_madeHearts;
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

        public int GetNumberOfPopedBubbles()
        {
            return m_numOfPopedBubbles;
        }

        public void LoadNextLevel()
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int nextLevel = (currentLevel + 1) % SceneManager.sceneCountInBuildSettings;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(nextLevel);
            

        }

        public void RestartLevel()
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            Time.timeScale = 1.0f;
            Debug.Log("restarting");
            SceneManager.LoadScene(currentLevel);
        }

        public void GoMainMenu()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(m_mainMenuBuildIndex);
            
        }
    }
}

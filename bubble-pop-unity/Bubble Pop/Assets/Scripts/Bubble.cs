using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField] GameObject m_popEffect;
       
        PackageContent m_content;
        PackageContent m_initialPackage;
        
        BubbleSize m_bubbleSizeState = BubbleSize.Small;
        [SerializeField] float m_distance = 2f;

        private bool m_isDirectionChosen = false;
        private Vector2 m_flowingDirection = Vector2.zero;
        private Vector3 m_initialPos;
        private BubbleMovingState m_movingState = BubbleMovingState.GoingOut;
        [SerializeField] float m_flowingSpeed = 0.5f;

        [SerializeField] Sprite m_dataIcon;
        [SerializeField] Sprite m_callTimeIcon;
        [SerializeField] Sprite m_messageIcon;

        [SerializeField] TMP_Text m_text;
        [SerializeField] Image m_bubbleIcon;
        [SerializeField] RectTransform m_rectTransform;
        // Start is called before the first frame update
        void Awake()
        {
          
            m_text = GetComponentInChildren<TMP_Text>();
            m_content = new PackageContent();
            SetInitialPos();
            SetSize();
            SetBubbleUi();
            SetInitialPackage();
        }

        private void Update()
        {
            if (m_movingState == BubbleMovingState.GoingOut)
            {
                if (m_isDirectionChosen)
                {
                    Vector3 movement = new Vector3(m_flowingDirection.x, m_flowingDirection.y, 0) * Time.deltaTime * m_flowingSpeed;
                    transform.position =
                    transform.position + movement;
                    
                    if (Vector3.Distance(transform.position , m_initialPos)>= m_distance)
                    {
                        m_movingState = BubbleMovingState.GettingBack;
                        m_isDirectionChosen = false;
                       
                    }
                }
                else
                {
                    float horizontal = Random.Range(-1f, 1f);
                    float vertical = Random.Range(-1f, 1f);

                    m_flowingDirection = new Vector2(horizontal, vertical);
                    m_isDirectionChosen = true;
                }
            }
            else if (m_movingState == BubbleMovingState.GettingBack)
            {
                m_flowingDirection = m_initialPos - transform.position;
                Vector3 movement = new Vector3(m_flowingDirection.x, m_flowingDirection.y, 0) * Time.deltaTime * m_flowingSpeed;
                transform.position =
                transform.position + movement;

                if (Vector3.Distance(transform.position,m_initialPos) <= 0.5)
                {
                    m_movingState = BubbleMovingState.GoingOut;
                 
                }

            }

            Vector3 viewPortPos = Camera.main.WorldToViewportPoint(transform.position);
            if (viewPortPos.x < 0.1)
            {
                m_movingState = BubbleMovingState.GettingBack;
                m_isDirectionChosen = false;

            }
            else if (viewPortPos.x > 0.9)
            {
                m_movingState = BubbleMovingState.GettingBack;
                m_isDirectionChosen = false;

            }
            else if (viewPortPos.y > 0.9)
            {
                m_movingState = BubbleMovingState.GettingBack;
                m_isDirectionChosen = false;

            }
            else if (viewPortPos.y < 0.3)
            {
                m_movingState = BubbleMovingState.GettingBack;
                m_isDirectionChosen = false;

            }
        }

       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            m_movingState = BubbleMovingState.GettingBack;
        }

        private void SetBubbleUi()
        {
            m_text.text = m_content.GetPackageTextContent();
            if(m_content.GetDataContent() != null)
            {
                m_bubbleIcon.sprite = m_dataIcon;
                return;
            }

            if (m_content.GetCallTimeContetn() != null)
            {
                m_bubbleIcon.sprite = m_callTimeIcon;
                return;
            }

            if (m_content.GetMessageContent() != null )
            {

                m_rectTransform.sizeDelta = new Vector2(0.08f, 0.06f);
                m_bubbleIcon.sprite = m_messageIcon;
                return;
            }
        }

        private void SetSizeState()
        {
            int numOfContentTypes = m_content.GetNumOfContents();

            if (numOfContentTypes <= 1)
            {
                m_bubbleSizeState = BubbleSize.Small;
            }

            else if (numOfContentTypes == 2)
            {
                m_bubbleSizeState = BubbleSize.Medium;
            }
            else 
            {
                m_bubbleSizeState = BubbleSize.Big;
            }
        }

        private void SetSize()
        {
            SetSizeState();
            switch(m_bubbleSizeState)
            {
                case BubbleSize.Small:
                    transform.localScale = new Vector3(0.5f, 0.5f,0.5f);
                    
                    break;
                case BubbleSize.Medium:
                    transform.localScale = new Vector3(1, 1, 1);
                    
                    break;
                case BubbleSize.Big:
                
                    transform.localScale = new Vector3(3, 3, 1);
                    break;
            }
            
        }

        public void Pop()
        {
            LevelManager.m_instance.AddItem(this);
            GameObject popEffect = Instantiate(m_popEffect, transform.position, Quaternion.identity);
            Destroy(popEffect, 2f);
            //gameObject.SetActive(false);
        }

        public void TutorialPop()
        {
            Destroy(this.gameObject);
        }

        public Data GetBubbleData()
        {
            return m_content.GetDataContent();
        }

        public CallTime GetBubbleCallTime()
        {
            return (m_content.GetCallTimeContetn());
        }

        public Message GetBubbleMessage()
        {
            return (m_content.GetMessageContent());
        }

        public void ActivateAnarestanPowerUp()
        {
            Data newData = null;
            CallTime newCallTime = null;
            Message newMessage = null;

            if (m_content.GetDataContent() != null)
            {
                newData = new Data((int)2 * GetBubbleData().GetData());
            }

            if (m_content.GetCallTimeContetn() != null)
            {
                newCallTime = new CallTime((int)2 * GetBubbleCallTime().GetCallTime());
            }

            if (m_content.GetMessageContent() != null)
            {
                newMessage = new Message((int)2 * GetBubbleMessage().GetMessageCount());
            }

            m_content = new PackageContent(newData,newCallTime,newMessage);
            SetSize();
            SetBubbleUi();

        }

        public void DeactivateAnarestanPowerUp()
        {

            Debug.Log("Deactivating power up");
            m_content = new PackageContent(m_initialPackage.GetDataContent(), m_initialPackage.GetCallTimeContetn(), m_initialPackage.GetMessageContent());
            SetSize();
            SetBubbleUi();
        }

        private void SetInitialPackage()
        {
            Data data = null;
            CallTime callTime = null;
            Message message = null;

            if (m_content.GetDataContent() != null)
            {
                data = new Data(m_content.GetDataContent().GetData());
            }

            if (m_content.GetCallTimeContetn() != null)
            {
                callTime = new CallTime(m_content.GetCallTimeContetn().GetCallTime());
            }

            if (m_content.GetMessageContent() != null)
            {
                message = new Message(m_content.GetMessageContent().GetMessageCount());
            }

            m_initialPackage = new PackageContent(data, callTime, message);
        }

        public void SetInitialPos()
        {
            m_initialPos = transform.position;
        }

        


    }

    enum BubbleSize
    {
        Small,Medium , Big
    }

    enum BubbleMovingState
    {
        GoingOut , GettingBack
    }
}

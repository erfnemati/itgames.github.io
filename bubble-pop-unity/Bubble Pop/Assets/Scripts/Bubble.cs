using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Assets.Scripts
{
    public class Bubble : MonoBehaviour
    {
        [SerializeField] GameObject m_popEffect;
       
        PackageContent m_content;
        
        BubbleSize m_bubbleSizeState = BubbleSize.Small;
        [SerializeField] float m_distance = 2f;

        private bool m_isDirectionChosen = false;
        private Vector2 m_flowingDirection = Vector2.zero;
        private Vector3 m_initialPos;
        private BubbleMovingState m_movingState = BubbleMovingState.GoingOut;
        [SerializeField] float m_flowingSpeed = 0.5f;



        [SerializeField] TMP_Text m_text;
        // Start is called before the first frame update
        void Awake()
        {
          
            m_text = GetComponentInChildren<TMP_Text>();
            m_content = new PackageContent();
            m_initialPos = transform.position;
            SetSize();
            SetText();
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
            else if (viewPortPos.y < 0.4)
            {
                m_movingState = BubbleMovingState.GettingBack;
                m_isDirectionChosen = false;

            }
        }

       
        private void OnTriggerEnter2D(Collider2D collision)
        {
            m_movingState = BubbleMovingState.GettingBack;
        }

        private void SetText()
        {
            m_text.text = m_content.GetPackageTextContent();
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
                    transform.localScale = new Vector3(2, 2, 1);
                    
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
            gameObject.SetActive(false);
        }

        public Data GetBubbleData()
        {
            return m_content.GetDataContent();
        }

        public CallTime GetBubbleCallTime()
        {
            return (m_content.GetCallTime());
        }

        public Message GetBubbleMessage()
        {
            return (m_content.GetMessage());
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

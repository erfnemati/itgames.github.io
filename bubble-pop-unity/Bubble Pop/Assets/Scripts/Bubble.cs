using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Assets.Scripts
{
    public class Bubble : MonoBehaviour
    {
        PackageContent m_content;
        
        BubbleSize m_bubbleSizeState = BubbleSize.Small;



        [SerializeField] TMP_Text m_text;
        // Start is called before the first frame update
        void Awake()
        {
            m_text = GetComponentInChildren<TMP_Text>();
            m_content = new PackageContent();
            SetSize();
            SetText();
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
                    transform.localScale = new Vector3(1.5f, 1.5f, 1);
                    
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
}

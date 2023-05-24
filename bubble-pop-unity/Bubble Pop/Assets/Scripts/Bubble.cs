using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Assets.Scripts
{
    public class Bubble : MonoBehaviour
    {
        private int m_typesOfContentCount = 0;
        PackageContent m_content;
        BubbleSize m_bubbleSizeState = BubbleSize.Small;


        [SerializeField] TMP_Text m_text;
        // Start is called before the first frame update
        void Start()
        {
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
            m_typesOfContentCount = m_content.GetNumOfContents();

            if (m_typesOfContentCount <= 1)
            {
                m_bubbleSizeState = BubbleSize.Small;
            }

            else if (m_typesOfContentCount == 2)
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
                    transform.localScale = new Vector3(1, 1, 1);
                    
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
            gameObject.SetActive(false);
        }
    }

    enum BubbleSize
    {
        Small,Medium , Big
    }
}

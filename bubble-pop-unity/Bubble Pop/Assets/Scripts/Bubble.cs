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
            m_content = new PackageContent(1);
            SetSize();
            SetPackageText();
        }

      
        private void SetPackageText()
        {
            string dataText;
            string callTimeText;
            string messagesCount;
            string packageContent;
            
           
            dataText = m_content.getPackageData() + "GB\n";
            callTimeText = m_content.getPackageCallTime() + "Mins\n";
            messagesCount = m_content.getPackageMessages() + "SMS";

            if (m_content.getPackageData() == 0)
            {
                dataText = null;
            }
            if (m_content.getPackageCallTime() == 0)
            {
                callTimeText = null;
            }
            if (m_content.getPackageMessages() == 0)
            {
                messagesCount = null;
            }

            packageContent = dataText + callTimeText + messagesCount;

            m_text.text = packageContent;
        }

        private void SetSizeState()
        {
            if (m_content.getPackageData() != 0) m_typesOfContentCount++;
            if (m_content.getPackageCallTime() != 0) m_typesOfContentCount++;
            if (m_content.getPackageMessages() != 0) m_typesOfContentCount++;

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

        // Update is called once per frame
        void Update()
        {

        }
    }

    enum BubbleSize
    {
        Small,Medium , Big
    }
}

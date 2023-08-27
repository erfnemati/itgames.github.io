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
        
        [SerializeField] Sprite m_dataIcon;
        [SerializeField] Sprite m_callTimeIcon;
        [SerializeField] Sprite m_messageIcon;

        [SerializeField] TMP_Text m_text;
        [SerializeField] Image m_bubbleIcon;
        [SerializeField] RectTransform m_rectTransform;

        [SerializeField] float m_finalScale;
        // Start is called before the first frame update
        void Awake()
        {
          
            m_text = GetComponentInChildren<TMP_Text>();
            m_content = new PackageContent();
            //SetInitialPos();
            SetSize();
            SetBubbleUi();
            SetInitialPackage();
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

        private void SetSize()
        {
            this.transform.localScale = new Vector3(m_finalScale, m_finalScale, m_finalScale);
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
    }

    enum BubbleSize
    {
        Small,Medium , Big
    }
}

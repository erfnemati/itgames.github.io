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


        Request m_currentReqquest;
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
            SetRequest();
            m_proposal = FindObjectOfType<Proposal>();
        }

        public void AddItem(Bubble bubble)
        {
            m_proposal.AddBubble(bubble);
        }

        private void SetRequest()
        {
            m_currentReqquest = RequestManager.m_instance.GetNewRequest();
            SetRequestText();
        }

        private void SetRequestText()
        {
            m_requestText.text = "I need \n";
            if (m_currentReqquest.GetRequestData().GetData() != 0)
            {
                m_requestText.text += m_currentReqquest.GetRequestData().GetData() + "GB\n";
            }

            if (m_currentReqquest.GetRequestCallTime().getCallTime() != 0)
            {
                m_requestText.text += m_currentReqquest.GetRequestCallTime().getCallTime() + "Mins\n";
            }

            if (m_currentReqquest.GetRequestMessage().GetMessageCount() != 0)
            {
                m_requestText.text += m_currentReqquest.GetRequestMessage().GetMessageCount() + "SMS";
            }
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

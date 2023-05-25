using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager m_instance;



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

            m_proposal = FindObjectOfType<Proposal>();
        }

        public void AddItem(Bubble bubble)
        {
            m_proposal.AddBubble(bubble);
        }

    }
}

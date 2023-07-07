using UnityEngine;
using System.Collections.Generic;

namespace Assets.WorkAtHome.Scripts
{
    class TutorialManager:MonoBehaviour
    {
        public static TutorialManager m_instance;

        List<ISequence> m_sequences = new List<ISequence>();
        
        private ISequence m_currentSequence;
        private int sequenceIndex = 0;


        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
                return;
            }
            Destroy(this.gameObject);
        }
        private void Start()
        {
            AddSequences();
            m_currentSequence = m_sequences[sequenceIndex];
            PlaySequence();
        }

        private void AddSequences()
        {
            m_sequences.Add(FindObjectOfType<ChildCustomerArrival>());
       
            m_sequences.Add(FindObjectOfType<DialogueOne>());
            
            m_sequences.Add(FindObjectOfType<DialogueTwo>());
        }

        public void PlaySequence()
        {
            m_currentSequence.PlaySequence();
        }
        public void GoNextSequence()
        {
            
            if (sequenceIndex >= m_sequences.Count)
            {
                Debug.Log("Everysequence has been played");
                return;
            }

            sequenceIndex++;
            m_currentSequence = m_sequences[sequenceIndex];
            PlaySequence();

        }

    }
}

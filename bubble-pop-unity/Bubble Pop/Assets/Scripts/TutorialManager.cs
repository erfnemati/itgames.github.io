using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.SceneManagement;

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

            m_sequences.Add(FindObjectOfType<DialogueThree>());

            m_sequences.Add(FindObjectOfType<TapAnar>());

            m_sequences.Add(FindObjectOfType<BubbleGeneration>());

            Debug.Log("Number of sequences are : " + m_sequences.Count);

        }

        public void PlaySequence()
        {

            m_currentSequence.PlaySequence();
        }
        public void GoNextSequence()
        {
            

            if (sequenceIndex >= m_sequences.Count- 1)
            {
                Debug.Log("Everysequence has been played");
                return;
            }

            sequenceIndex++;
            Debug.Log("Sequence index is : " + sequenceIndex);
            m_currentSequence = m_sequences[sequenceIndex];
            PlaySequence();

        }

        public void ChangeSequence()
        {
            Debug.Log("Hello from tutorial manager");
            m_currentSequence.ChangeSequence();
        }

        public void LoadNextLevel()
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int nextLevel = (currentLevel + 1) % SceneManager.sceneCount;
            SceneManager.LoadScene(nextLevel);
        }

        

    }
}

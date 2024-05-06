using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutotrial
{
    public class TutorialManager : MonoBehaviour
    {

        [SerializeField] List<GameObject> m_stateList = new List<GameObject>();
        private GameObject m_currentState;
        private int m_lastListIndex = 0;

        private void Start()
        {
            m_currentState = m_stateList[m_lastListIndex];
        }

        public void SkipTutorial() => ServiceLocator._instance.Get<BmsPlusSceneManager>().RestartLevel();
        public void RetryTutorial() => ServiceLocator._instance.Get<BmsPlusSceneManager>().LoadScene(SceneName.TutorialScene);

        public void LoadNextState()
        {
            m_lastListIndex = (m_lastListIndex + 1) % m_stateList.Count;
            //m_currentState.SetActive(false);
            Destroy(m_currentState.gameObject);
            //The worst code here :
            Player._instance = null;
            m_stateList[m_lastListIndex].SetActive(true);
            m_currentState = m_stateList[m_lastListIndex];


        }

    }

}
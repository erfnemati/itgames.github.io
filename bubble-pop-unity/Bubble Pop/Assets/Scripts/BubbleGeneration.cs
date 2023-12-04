using Assets.WorkAtHome.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class BubbleGeneration : MonoBehaviour, ISequence
    {

        private bool m_isSequenceStarted = false;
        private bool m_isPowerUpEnabled = false;
        [SerializeField] GameObject m_dialogueBox;
        [SerializeField] int m_numOfBubbles;
        [SerializeField] GameObject m_bubblePrefab;
        [SerializeField] List<Transform> m_bubbleTransfroms;
        [SerializeField] List<GameObject> m_generatedBubbles;
        public void ChangeSequence()
        {
            throw new NotImplementedException();
        }

        public void PlaySequence()
        {
            GenerateBubbles();
            m_isSequenceStarted = true;
        }

        public void GenerateBubbles()
        {
            for(int i = 0; i <m_numOfBubbles; i++)
            {
                GameObject generatedObject = Instantiate(m_bubblePrefab,m_bubbleTransfroms[i].position,Quaternion.identity);
                m_generatedBubbles.Add(generatedObject);
            }
        }

        private void Update()
        {
            if (m_isSequenceStarted)
            {
            
                GetInput();
            }
        }

        private void GetInput()
        {
            if (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Collider2D selectedObject = Physics2D.OverlapPoint(worldPos);

                if (selectedObject != null && selectedObject.CompareTag("AnarPowerUpIcon"))
                {
                    for(int i = 0; i < selectedObject.transform.childCount; i++)
                    {
                        selectedObject.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    SwitchPowerUp();
                }
                if (m_isPowerUpEnabled)
                {
                    if (selectedObject != null && selectedObject.CompareTag("Bubble"))
                    {
                        
                        
                        selectedObject.GetComponent<Bubble>().TutorialPop();
                        m_generatedBubbles.Remove(selectedObject.gameObject);
                        m_numOfBubbles--;
                        if(m_numOfBubbles <= 0)
                        {
                            Debug.Log("Level finished");
                            TutorialManager.m_instance.LoadNextLevel();
                        }
                            
                        
                    }
                }
            }
        }

        public void SwitchPowerUp()
        {
            
            if (m_isPowerUpEnabled)
            {
                m_isPowerUpEnabled = false;
                foreach (GameObject temp in m_generatedBubbles)
                {
                    Debug.Log("Activating Power Up");
                    temp.GetComponent<Bubble>().DeactivateAnarestanPowerUp();
                }
                Debug.Log("Power up is diabled");
            }

            else
            {
                m_isPowerUpEnabled = true;
                foreach(GameObject temp in m_generatedBubbles)
                {
                    Debug.Log("Activating Power Up");
                    temp.GetComponent<Bubble>().ActivateAnarestanPowerUp();
                }
                Debug.Log("Power up is enabled");
            }
        }

        private void ActivatePowerUp()
        {
            foreach(GameObject temp in m_generatedBubbles)
            {
                Debug.Log("Activate Power Up");
            }
        }
    }
}

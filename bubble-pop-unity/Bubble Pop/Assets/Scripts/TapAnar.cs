using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


namespace Assets.WorkAtHome.Scripts
{

    public class TapAnar : MonoBehaviour,ISequence
    {
        private bool m_isSequenceStarted = false;
        private bool m_isSequenceFinished = false;
        [SerializeField] private int m_numOfTaps;

        private GameObject m_bigAnar;
        private GameObject m_anarPowerUpIcon;
        [SerializeField] string m_sequenceDialogue;
        [SerializeField] GameObject m_dialogueBox;
        [SerializeField] Slider m_powerUpSlider;
        [SerializeField] GameObject m_powerUp;
        [SerializeField] Transform m_powerUpIconPos;
        [SerializeField] float m_powerUpIconCycle;
        public void ChangeSequence()
        {
            m_dialogueBox.gameObject.SetActive(false);
            for (int i = 0; i< m_anarPowerUpIcon.transform.childCount; i++)
            {
                m_anarPowerUpIcon.transform.GetChild(i).gameObject.SetActive(true);
            }
            m_anarPowerUpIcon.GetComponentInChildren<TMP_Text>().text = "Tap ME";
            
            TutorialManager.m_instance.GoNextSequence();
            Destroy(this);
        }

        public void PlaySequence()
        {
            m_isSequenceStarted = true;
        }

        private void Update()
        {
            if (m_isSequenceStarted)
            {
                if (IsTapping())
                {
                    UpdateSlider();
                    UpdateNumOfTaps();

                }
            }

            if (m_isSequenceFinished)
            {
                if (IsPowerUpIconGrabbed())
                {
                    Debug.Log("Grabbed");
                    m_anarPowerUpIcon.gameObject.transform.DOMove(m_powerUpIconPos.position, m_powerUpIconCycle).OnComplete(()=>ChangeSequence());
                }
            }
        }

        private bool IsPowerUpIconGrabbed()
        {
            if (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Collider2D selectedGameObject = Physics2D.OverlapPoint(new Vector2(worldPos.x, worldPos.y));
                if (selectedGameObject != null && selectedGameObject.CompareTag("AnarPowerUpIcon"))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsTapping()
        {
            if (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Collider2D selectedGameObject = Physics2D.OverlapPoint(new Vector2(worldPos.x, worldPos.y));
                if (selectedGameObject != null && selectedGameObject.CompareTag("AnarPowerUp"))
                {
                    //Debug.Log("Tapped");
                    if (m_bigAnar == null)
                    {
                        m_bigAnar = selectedGameObject.gameObject;
                    }
                    return true;
            
                }
            }
            return false;
        }

        private void UpdateSlider()
        {
           
        }

        private void UpdateNumOfTaps()
        {
            
            m_numOfTaps--;
            if (m_numOfTaps <= 0)
            {
                ActivatePowerUp();
            }
        }

        private void ActivatePowerUp()
        {
            m_anarPowerUpIcon = Instantiate(m_powerUp);
            m_dialogueBox.SetActive(true);
            m_dialogueBox.GetComponentInChildren<TMP_Text>().text = m_sequenceDialogue;
            Destroy(m_bigAnar);
            m_isSequenceFinished = true;

        }
    }
}

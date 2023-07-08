using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Assets.WorkAtHome.Scripts
{
    class DialogueThree : MonoBehaviour, ISequence
    {
        [SerializeField] string m_dialogueText;
        [SerializeField] GameObject m_dialogueBox;
        [SerializeField] GameObject m_anarPowerUp;
        [SerializeField] float m_finalPowerUpScale;
        [SerializeField] float m_scaleDuration;

        private bool m_isPowerUpInstantiated = false;
        private bool m_isSequenceFinished = false;

        private void Update()
        {
            if (m_isPowerUpInstantiated == true && m_isSequenceFinished == false)
            {
                CheckForTap();
            }
        }

        private void CheckForTap()
        {
            if (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Collider2D selectedGameObject = Physics2D.OverlapPoint(new Vector2(worldPos.x, worldPos.y));
                if (selectedGameObject != null && selectedGameObject.CompareTag("AnarPowerUp"))
                {
                    ChangeSequence();
                    m_isSequenceFinished = true;
                }
            }
        }
        public void ChangeSequence()
        {
            if (m_dialogueBox)
            {
                Debug.Log("Deactivating here");
                m_dialogueBox.SetActive(false);
            }
            TutorialManager.m_instance.GoNextSequence();
        }

        public void PlaySequence()
        {
            
            m_dialogueBox.SetActive(true);

            ShowCustomerDialogue();
            Invoke(nameof(InstantiatePowerUp), 0.5f);

            //Debug.Log("I am here 2");
        }

        public void ShowCustomerDialogue()
        {
           if (m_dialogueBox.GetComponentInChildren<TMP_Text>()!= null)
            {
                m_dialogueBox.GetComponentInChildren<TMP_Text>().text = m_dialogueText;
            }
            //Debug.Log("I am here 1");
        }

        private void InstantiatePowerUp()
        {
            GameObject instantiatedObject = Instantiate(m_anarPowerUp);
            instantiatedObject.transform.DOScale(m_finalPowerUpScale, m_scaleDuration);
            m_isPowerUpInstantiated = true;
            //Debug.Log("I am here 3");
        }
    }
}

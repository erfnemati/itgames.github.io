using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        AudioSource m_audioSource;
        [SerializeField] AudioClip m_popSound;
        private bool m_isPaused = false;
        public static InputHandler m_instance;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            if (m_instance == null)
            {
                m_instance = this;
                return;
            }
            Destroy(m_instance.gameObject);
            
        }
        // Start is called before the first frame update
        void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
            
        }

        private void Update()
        {
            if (m_isPaused)
            {
                return;
            }
            if (Input.touchCount != 0  && Input.touches[0].phase == TouchPhase.Began)
            {
                
                Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Collider2D selectedObject = Physics2D.OverlapPoint(new Vector2(worldTouchPos.x,worldTouchPos.y));
                if (selectedObject != null && selectedObject.CompareTag("Bubble"))
                {
                    Debug.Log("Bubble is poped");
                    selectedObject.GetComponent<Bubble>().Pop();
                    m_audioSource.clip = m_popSound;
                    m_audioSource.Play();
                    
                    
                }
                if (selectedObject != null && selectedObject.CompareTag("SendButton"))
                {
                    Debug.Log("SendButton pressed");
                    LevelManager.m_instance.SendProposal();
                }


                if (selectedObject != null && selectedObject.CompareTag("Customer"))
                {
                    //Debug.Log("Customer Selected");
                    //LevelManager.m_instance.GetNewCustomer();
                }

                
            }
        }

        public void SwitchIsPaused()
        {
            if (!m_isPaused)
            {
                m_isPaused = true;
                Time.timeScale = 0.0f;
                Debug.Log("Game is paused");
                return;
            }
            m_isPaused = false;
            Time.timeScale = 1.0f;
            Debug.Log("Game is unpaused");

        }

    }
}

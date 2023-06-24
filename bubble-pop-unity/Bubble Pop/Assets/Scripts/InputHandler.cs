using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        AudioSource m_audioSource;
        [SerializeField] AudioClip m_popSound;
        // Start is called before the first frame update
        void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.touchCount != 0  && Input.touches[0].phase == TouchPhase.Began)
            {
                
                Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Collider2D selectedObject = Physics2D.OverlapPoint(new Vector2(worldTouchPos.x,worldTouchPos.y));
                if (selectedObject != null && selectedObject.CompareTag("Bubble"))
                {
                    selectedObject.GetComponent<Bubble>().Pop();
                    m_audioSource.clip = m_popSound;
                    m_audioSource.Play();
                    
                    
                }
                if (selectedObject != null && selectedObject.CompareTag("SendButton"))
                {
                    Debug.Log("SendButton pressed");
                    LevelManager.m_instance.SendProposal();
                }

                if (selectedObject != null && selectedObject.CompareTag("DiscardButton"))
                {
                    Debug.Log("DiscardButton pressed");
                    LevelManager.m_instance.DiscardProposal();
                }

                if (selectedObject != null && selectedObject.CompareTag("Customer"))
                {
                    Debug.Log("Customer Selected");
                    LevelManager.m_instance.GetNewCustomer();
                }

                
            }
        }

    }
}

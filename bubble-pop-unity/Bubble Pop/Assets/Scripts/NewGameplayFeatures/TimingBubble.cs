using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class TimingBubble : MonoBehaviour
    {
        [SerializeField] float m_floatingSpeed;
        private Vector3 m_floatingDirection;

        private void Start()
        {
            m_floatingDirection = new Vector3(Random.Range(-1, 1), Random.Range(0, 1f), 0);
            Debug.Log("New direction");
        }

        private void Update()
        {
            MoveBubble();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("LevelBoundry"))
            {
                Debug.Log("Bye");
                LevelManager.m_instance.RemoveBubbleOnly(GetComponent<Bubble>());
            }
        }

        private void MoveBubble()
        {
            Vector3 thisFrameMovement = m_floatingDirection * m_floatingSpeed * Time.deltaTime;
            transform.position += thisFrameMovement;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class ScalableBubble : MonoBehaviour
    {
        [SerializeField] float m_finalScale;
        [SerializeField] float m_scaleSpeed;
        
        private Vector3 m_initialScale;

        private void Awake()
        {
            m_initialScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
        }

        private void Update()
        {
            MakeBubbleSmaller();
        }

        private void MakeBubbleSmaller()
        {
            transform.localScale += m_scaleSpeed * new Vector3(-1, -1, -1) * Time.deltaTime;
            CheckDestroyTime();
            
        }

        private void CheckDestroyTime()
        {
            if (transform.localScale.x <= 0.01f)
            {
                LevelManager.m_instance.RemoveBubbleOnly(GetComponent<Bubble>());
                Destroy(this.gameObject);

            }
        }
        
        public void SetInitialScale()
        {
            Debug.Log("Initial scale is : " + m_initialScale);
            transform.localScale = m_initialScale;
        }
    }
}

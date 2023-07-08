using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Assets.WorkAtHome.Scripts
{
    public class ChildCustomerArrival : MonoBehaviour,ISequence
    {
        [SerializeField] Transform m_finalPos;
        [SerializeField] float m_cycle;

        [SerializeField] Transform m_customerTransform;
        public void ChangeSequence()
        {
            Debug.Log("Customer arrival sequence has been finished");
            TutorialManager.m_instance.GoNextSequence();
        }

        public void PlaySequence()
        {
            Debug.Log("Customer arrival sequence started");
            m_customerTransform.DOMoveX(m_finalPos.position.x, m_cycle).OnComplete(()=>ChangeSequence());
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

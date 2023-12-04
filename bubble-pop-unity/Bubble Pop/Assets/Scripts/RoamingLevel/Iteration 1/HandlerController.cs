using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandlerController : MonoBehaviour
{
    [SerializeField] GameObject m_innerHandler;
    [SerializeField] float m_maxDegree;
    [SerializeField] float m_minDegree;
    public void RotateTowards(Vector3 targetPos)
    {
        transform.up = targetPos - transform.position;
        if (transform.eulerAngles.z >= m_maxDegree)
        {
            
            transform.localEulerAngles = new Vector3(0, 0, 329f);
            
        }

        if(transform.eulerAngles.z <= m_minDegree && transform.eulerAngles.z >= Mathf.Epsilon)
        {
            
            transform.localEulerAngles = new Vector3(0, 0, 275f);
         
        }
    }

    public void ActivateInnerHandler()
    {
        m_innerHandler.SetActive(true);
        m_innerHandler.transform.DOScaleX(1f, 1f);

    }

    
}

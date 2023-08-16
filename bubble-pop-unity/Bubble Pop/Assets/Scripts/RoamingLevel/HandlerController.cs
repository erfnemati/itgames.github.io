using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandlerController : MonoBehaviour
{
    [SerializeField] GameObject m_innerHandler;
    public void RotateTowards(Vector3 targetPos)
    {
        Debug.Log("Rotation is : " + transform.eulerAngles.z);
        transform.up = targetPos - transform.position;
        if (transform.eulerAngles.z >= 330f)
        {
            
            transform.localEulerAngles = new Vector3(0, 0, 329f);
            
        }

        if(transform.eulerAngles.z <= 270f && transform.eulerAngles.z >= Mathf.Epsilon)
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

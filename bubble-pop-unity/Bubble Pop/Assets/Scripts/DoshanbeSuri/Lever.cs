using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject m_handler;
    private void OnEnable()
    {
        Vector3 initialScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
        transform.localScale = Vector3.zero;

        transform.DOScale(initialScale, 1f).OnComplete(() => ActivateHandle());
        

    }

    private void ActivateHandle()
    {
        m_handler.SetActive(true);
    }

    

    
}

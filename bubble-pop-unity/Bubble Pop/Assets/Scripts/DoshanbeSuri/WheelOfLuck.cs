using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelOfLuck : MonoBehaviour
{

    private void OnEnable()
    {
        PopAnimation();
        //Debug.Log("Hello");
    }

    private void PopAnimation()
    {
        float m_initialScale = transform.localScale.x;
        transform.localScale = Vector3.zero;
        Debug.Log("Scale is : " + m_initialScale);
        transform.DOScale(new Vector3(0.8f,0.8f,0.8f), 1f);
    }
}

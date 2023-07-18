using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Astronut : MonoBehaviour
{
    [SerializeField] Transform m_finalAstronutPos;
    [SerializeField] float m_transformationCycle;
    [SerializeField] Shuttle m_shuttleScript;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOMove(m_finalAstronutPos.position, m_transformationCycle).OnComplete(()=>StartShuttle());
    }

    private void StartShuttle()
    {
        this.gameObject.SetActive(false);
        m_shuttleScript.ChangeSprite();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Astronut2 : MonoBehaviour
{
    [SerializeField] Transform m_astronutFinalPos;
    [SerializeField] float cycleTime;
    [SerializeField] Moon m_moonScript;

    // Start is called before the first frame update
    private void Start()
    {
        this.transform.DOMove(m_astronutFinalPos.position, cycleTime).OnComplete(()=>ActivateMoon());
    }

    private void ActivateMoon()
    {
        m_moonScript.StartTransformation();
    }
}

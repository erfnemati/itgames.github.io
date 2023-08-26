using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoamingAstronut : MonoBehaviour
{
    [SerializeField] Transform m_finalTransfrom;
    [SerializeField] float m_translationCycleTime;
    [SerializeField] RoamingGameManager m_roamingGameManager;
    public void GoToShuttle()
    {
        transform.DOMove(m_finalTransfrom.position, m_translationCycleTime).OnComplete(()=>BoardShuttle());
    }

    public void BoardShuttle()
    {

        m_roamingGameManager.StartShuttle();
        Destroy(this.gameObject);
    }

    
}

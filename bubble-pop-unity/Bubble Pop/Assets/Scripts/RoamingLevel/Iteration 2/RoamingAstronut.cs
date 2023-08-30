using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class RoamingAstronut : MonoBehaviour
{
    [SerializeField] Transform m_finalTransfrom;
    [SerializeField] float m_translationCycleTime;
    [SerializeField] RoamingGameManager m_roamingGameManager;
    public async Task GoToShuttle()
    {
        await transform.DOMove(m_finalTransfrom.position, m_translationCycleTime).AsyncWaitForCompletion();
    }
}

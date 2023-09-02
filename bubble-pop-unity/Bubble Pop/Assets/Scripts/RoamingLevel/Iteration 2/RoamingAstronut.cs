using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class RoamingAstronut : MonoBehaviour
{
    [SerializeField] Transform m_finalTransfrom;
    [SerializeField] float m_translationCycleTime;

    [SerializeField] RoamingAstronutUi m_astronutUi;

    [SerializeField] Transform m_initialArrivalTransform;
    private Vector3 m_initialPos;

    private void Start()
    {
        m_initialPos = transform.position;
    }
    public async Task GoToShuttle()
    {
        await transform.DOMove(m_finalTransfrom.position, m_translationCycleTime).AsyncWaitForCompletion();

    }

    public void ShowDialogueBox()
    {
        m_astronutUi.ShowAstronutDialogue();
    }

    public void CloseDialogueBox()
    {
        m_astronutUi.CloseDialogueBox();
    }

    public async Task ArriveForFirstTime()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(m_initialArrivalTransform.position, 4f));
        sequence.AppendInterval(2f);
        await sequence.AsyncWaitForCompletion();
        ShowDialogueBox();
        await Task.Delay(2000);
    }

    public void GetToInitialPlace()
    {
        transform.DOMove(m_initialPos, 3f);
    }
    public void ExitScreen()
    {
        transform.DOMove(m_initialPos, 1f);
    }

    
}

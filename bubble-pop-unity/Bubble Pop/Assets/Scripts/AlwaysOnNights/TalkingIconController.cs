using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TalkingIconController : MonoBehaviour
{
    private void Start()
    {
        AnimateTalkingIcon();
    }
    private void AnimateTalkingIcon()
    {

        transform.DOMove(this.transform.localPosition - new Vector3(0.1f, 0, 0), 0.75f).SetLoops(-1, LoopType.Yoyo);
        transform.DOScale(0, 0.75f).SetLoops(-1, LoopType.Yoyo);
    }
    
}

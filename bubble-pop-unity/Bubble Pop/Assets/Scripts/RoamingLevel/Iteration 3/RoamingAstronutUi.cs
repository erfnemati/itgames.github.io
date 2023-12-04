using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RoamingAstronutUi : MonoBehaviour
{
    [SerializeField] Canvas m_astronutCanvas;
    [SerializeField] GameObject m_astronutDialogueBox;

    [SerializeField] Button m_acceptButton;

    public void ShowAstronutDialogue()
    {
        m_astronutCanvas.gameObject.SetActive(true);
        //m_astronutDialogueBox.GetComponent<RectTransform>().DOScale(1f,0.5f);

        //m_acceptButton.GetComponent<RectTransform>().DOScale(1f, 0.5f);
    }

    public void CloseDialogueBox()
    {
        m_astronutCanvas.gameObject.SetActive(false);
    }
}

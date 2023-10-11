using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoshanbeSuriTutorialManager : MonoBehaviour
{

    public static DoshanbeSuriTutorialManager _instance;
    [SerializeField] float offset = 0.5f;
    [SerializeField] GameObject m_tutorialHand;
    [SerializeField] GameObject m_tutorialDialogueBox;


    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Hello");
            _instance = this;
        }

        ActivateDialogueBox();
        ActivateTutorialHand();
    }

    private void ActivateDialogueBox()
    {
        Vector3 initialScale = new Vector3 (m_tutorialDialogueBox.transform.localScale.x,
                                            m_tutorialDialogueBox.transform.localScale.y,
                                            m_tutorialDialogueBox.transform.localScale.z);

        m_tutorialDialogueBox.gameObject.SetActive(true);
        m_tutorialDialogueBox.transform.localScale = Vector3.zero;
        m_tutorialDialogueBox.transform.DOScale(initialScale, 0.5f);
    }
    private void ActivateTutorialHand()
    {
        Vector3 initialPos = new Vector3(m_tutorialHand.transform.position.x,
                                         m_tutorialHand.transform.position.y,
                                         m_tutorialHand.transform.position.z);

        m_tutorialHand.gameObject.SetActive(true);
        m_tutorialHand.transform.DOMove(initialPos - new Vector3(0, offset, 0),1f).SetLoops(-1,LoopType.Yoyo);
    }

    public void DeactivateTutorials()
    {
        m_tutorialHand.gameObject.SetActive(false);
        m_tutorialDialogueBox.gameObject.SetActive(false);
    }
}

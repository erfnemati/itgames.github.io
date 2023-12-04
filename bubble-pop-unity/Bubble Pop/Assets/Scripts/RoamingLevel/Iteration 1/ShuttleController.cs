using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShuttleController : MonoBehaviour
{
    public static ShuttleController _instance;

    [SerializeField] Sprite m_startedShuttleSprite;
    [SerializeField] Transform m_targetTransform;
    [SerializeField] float m_translateSpeed;
    [SerializeField] float m_translationCycleTime;
    [SerializeField] float m_finalScale;

    [SerializeField] GameObject Pipe;

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void StartShuttle()
    {
        GetComponent<SpriteRenderer>().sprite = m_startedShuttleSprite;
        Pipe.gameObject.SetActive(false);
        transform.DOMove(m_targetTransform.position, m_translationCycleTime);
        transform.DOScale(m_finalScale, m_translationCycleTime).OnComplete(()=>ShowResultMenu());
    }

    private void ShowResultMenu()
    {
        OverlayUiController._instance.ShowResultMenu();
    }
}

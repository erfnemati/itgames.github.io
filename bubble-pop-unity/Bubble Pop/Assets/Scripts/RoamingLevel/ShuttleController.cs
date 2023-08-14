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
        transform.DOMove(m_targetTransform.position, m_translationCycleTime);
        transform.DOScale(m_finalScale, m_translationCycleTime);
    }
}

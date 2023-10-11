using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoonBubble : MonoBehaviour
{
    private bool m_isFollowing = false;
    private AstronutAutoMovement m_astronutMovementController;

    [SerializeField] float m_followingSpeed;
    [SerializeField] float m_finalScale;
    [SerializeField] float m_freq=5f;
    [SerializeField] float magn=0.4f;
    [SerializeField] GameObject vfx;

    Vector3 pos, localscale;
    private void Start()
    {
        pos = transform.position;
        localscale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(m_finalScale, 0.5f);
        m_astronutMovementController = FindObjectOfType<AstronutAutoMovement>();
    }
    private void Update()
    {
        if (!m_isFollowing)
            transform.position = pos + transform.up * Mathf.Sin(Time.time * m_freq) * magn;

        if (m_isFollowing)
        {
            FollowAstronut();
        }
       // localscale *= -1;
       // transform.localScale = localscale;
    }

    private void FollowAstronut()
    {
        Vector3 direction = m_astronutMovementController.transform.position - this.transform.position;
        transform.position += direction * m_followingSpeed * Time.deltaTime;

        if (Vector3.Distance(this.gameObject.transform.position, m_astronutMovementController.transform.position) < Mathf.Epsilon)
        {
            m_isFollowing = false;
            AlwaysOnNigthsGameManager._instance.WinGame();

            Destroy(this.gameObject);

        }
    }
    public void PopMoon()
    {
        m_isFollowing = true;
    }


}

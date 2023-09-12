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

    
    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(m_finalScale, 0.5f);
        m_astronutMovementController = FindObjectOfType<AstronutAutoMovement>();
    }

    private void Update()
    {
        if (m_isFollowing)
        {
            FollowAstronut();
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBubble : MonoBehaviour
{
    private bool m_isDirectionChosen = false;
    private Vector2 m_flowingDirection = Vector2.zero;
    private Vector3 m_initialPos;
    private BubbleMovingState m_movingState = BubbleMovingState.GoingOut;
    [SerializeField] float m_flowingSpeed = 0.5f;
    [SerializeField] float m_distance = 2f;

    void Update()
    {
        MoveAround();
    }

    private void MoveAround()
    {

        if (m_movingState == BubbleMovingState.GoingOut)
        {
            if (m_isDirectionChosen)
            {
                Vector3 movement = new Vector3(m_flowingDirection.x, m_flowingDirection.y, 0) * Time.deltaTime * m_flowingSpeed;
                transform.position =
                transform.position + movement;

                if (Vector3.Distance(transform.position, m_initialPos) >= m_distance)
                {
                    m_movingState = BubbleMovingState.GettingBack;
                    m_isDirectionChosen = false;

                }
            }
            else
            {
                float horizontal = Random.Range(-1f, 1f);
                float vertical = Random.Range(-1f, 1f);

                m_flowingDirection = new Vector2(horizontal, vertical);
                m_isDirectionChosen = true;
            }
        }
        else if (m_movingState == BubbleMovingState.GettingBack)
        {
            m_flowingDirection = m_initialPos - transform.position;
            Vector3 movement = new Vector3(m_flowingDirection.x, m_flowingDirection.y, 0) * Time.deltaTime * m_flowingSpeed;
            transform.position =
            transform.position + movement;

            if (Vector3.Distance(transform.position, m_initialPos) <= 0.5)
            {
                m_movingState = BubbleMovingState.GoingOut;

            }

        }

        Vector3 viewPortPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPortPos.x < 0.1)
        {
            m_movingState = BubbleMovingState.GettingBack;
            m_isDirectionChosen = false;

        }
        else if (viewPortPos.x > 0.9)
        {
            m_movingState = BubbleMovingState.GettingBack;
            m_isDirectionChosen = false;

        }
        else if (viewPortPos.y > 0.9)
        {
            m_movingState = BubbleMovingState.GettingBack;
            m_isDirectionChosen = false;

        }
        else if (viewPortPos.y < 0.3)
        {
            m_movingState = BubbleMovingState.GettingBack;
            m_isDirectionChosen = false;

        }
    }

    public void SetInitialPos()
    {
        m_initialPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_movingState = BubbleMovingState.GettingBack;
    }

    enum BubbleMovingState
    {
        GoingOut, GettingBack
    }
}

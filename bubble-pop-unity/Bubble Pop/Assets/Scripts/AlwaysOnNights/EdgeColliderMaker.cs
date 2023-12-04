using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeColliderMaker : MonoBehaviour
{
    private Vector2 m_topLeft,m_topRight,m_bottomLeft,m_bottomRight;

    private Rigidbody2D m_rigidBody;
    private EdgeCollider2D m_topEdge, m_rightEdge, m_bottomEdge, m_leftEdge;

    private float m_screenHeight, m_screenWidth;

    private void Start()
    {
        GetDimentions();
        SpecifyCornerPoinsCordination();

        m_rigidBody = gameObject.AddComponent<Rigidbody2D>();
        m_rigidBody.bodyType = RigidbodyType2D.Kinematic;

        AddEdgeColliders();


    }

    private void GetDimentions()
    {
        m_screenHeight = GetComponent<RectTransform>().rect.height;
        m_screenWidth = GetComponent<RectTransform>().rect.width;
    }

    private void SpecifyCornerPoinsCordination()
    {
        m_topLeft = new Vector2(-m_screenWidth / 2, m_screenHeight / 2);
        m_topRight = new Vector2(m_screenWidth / 2, m_screenHeight / 2);

        m_bottomLeft = new Vector2(-m_screenWidth / 2, -m_screenHeight / 2);
        m_bottomRight = new Vector2(m_screenWidth / 2, -m_screenHeight / 2);

        
    }

    private void AddEdgeColliders()
    {
        m_topEdge = gameObject.AddComponent<EdgeCollider2D>();
        m_rightEdge = gameObject.AddComponent<EdgeCollider2D>();
        m_bottomEdge = gameObject.AddComponent<EdgeCollider2D>();
        m_leftEdge = gameObject.AddComponent<EdgeCollider2D>();

        m_topEdge.points = new[] { m_topLeft, m_topRight };
        m_rightEdge.points = new[] { m_topRight, m_bottomRight };
        m_bottomEdge.points = new[] { m_bottomLeft, m_bottomRight };
        m_leftEdge.points = new[] { m_bottomLeft, m_topLeft };

        m_topEdge.isTrigger = true;
        m_rightEdge.isTrigger = true;
        m_bottomEdge.isTrigger = true;
        m_leftEdge.isTrigger = true;

    }
    
}

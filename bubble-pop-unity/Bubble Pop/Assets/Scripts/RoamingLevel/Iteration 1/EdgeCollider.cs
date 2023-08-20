using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCollider : MonoBehaviour
{
    private float m_screenHeight;
    private float m_screenWidth;

    Vector2 m_topLeft, m_topRight, m_bottomRight, m_bottomLeft;

    EdgeCollider2D m_leftEdge, m_topEdge, m_rightEdge, m_bottomEdge;

    Rigidbody2D m_rigidBody;

    Vector3 m_triggerPos = new Vector3(0, 0, 0);

    void Start()
    {
        GetScreenDimentions();
        GetCornerCordinates();

        m_rigidBody = this.gameObject.AddComponent<Rigidbody2D>();
        m_rigidBody.bodyType = RigidbodyType2D.Kinematic;

        AddEdgeColliders();


    }


    private void GetScreenDimentions()
    {
        m_screenHeight = GetComponent<RectTransform>().rect.height;
        m_screenWidth = GetComponent<RectTransform>().rect.width;
    }

    private void GetCornerCordinates()
    {
        m_topLeft = new Vector2(-m_screenWidth / 2, m_screenHeight / 2);
        m_topRight = new Vector2(m_screenWidth / 2, m_screenHeight / 2);
        m_bottomRight = new Vector2(m_screenWidth / 2, -m_screenHeight / 2);
        m_bottomLeft = new Vector2(-m_screenWidth / 2, -m_screenHeight / 2);
    }
    
    private void AddEdgeColliders()
    {
        m_leftEdge = this.gameObject.AddComponent<EdgeCollider2D>();
        m_topEdge = this.gameObject.AddComponent<EdgeCollider2D>();
        m_rightEdge = this.gameObject.AddComponent<EdgeCollider2D>();
        m_bottomEdge = this.gameObject.AddComponent<EdgeCollider2D>();

        m_leftEdge.points = new[] { m_bottomLeft, m_topLeft };
        m_topEdge.points = new[] { m_topLeft, m_topRight };
        m_rightEdge.points = new[] { m_topRight, m_bottomRight };
        m_bottomEdge.points = new[] { m_bottomRight, m_bottomLeft };
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FuelBubble"))
        {
            Debug.Log("Triggered1");
            m_triggerPos = collision.gameObject.transform.position;
            collision.gameObject.transform.position = m_triggerPos;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FuelBubble"))
        {
            Debug.Log("Triggered2");
            collision.gameObject.transform.position = m_triggerPos;
        }
    }
}

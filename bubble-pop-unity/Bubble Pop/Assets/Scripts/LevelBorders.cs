using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBorders : MonoBehaviour
{
    Vector2 m_topRightCorner, m_topLeftCorner, m_bottomLeftCorner, m_bottomRightCorner;
    float m_screenWidth, m_screenHeight;
    EdgeCollider2D m_righEdge, m_leftEdge, m_topEdge, m_bottomEdge;
    Canvas m_canvasComponent;
    Rigidbody2D m_rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        m_righEdge = gameObject.AddComponent<EdgeCollider2D>();
        m_bottomEdge = gameObject.AddComponent<EdgeCollider2D>();
        m_leftEdge = gameObject.AddComponent<EdgeCollider2D>();
        m_topEdge = gameObject.AddComponent<EdgeCollider2D>();

        m_canvasComponent = GetComponent<Canvas>();
        m_rigidBody = gameObject.AddComponent<Rigidbody2D>();
        m_rigidBody.bodyType = RigidbodyType2D.Kinematic;

        GetDimentions();
        GetCornerCondinates();
        CreateEdgeColliders();
    }

    private void GetDimentions()
    {
        m_screenWidth = m_canvasComponent.GetComponent<RectTransform>().rect.width;
        m_screenHeight = m_canvasComponent.GetComponent<RectTransform>().rect.height;
    }

    private void GetCornerCondinates()
    {
        m_topLeftCorner = new Vector2(-m_screenWidth / 2, m_screenHeight / 2);
        m_topRightCorner = new Vector2(m_screenWidth / 2, m_screenHeight / 2);
        m_bottomLeftCorner = new Vector2(-m_screenWidth / 2, -m_screenHeight / 2);
        m_bottomRightCorner = new Vector2(m_screenWidth/2, -m_screenHeight / 2);

    }

    private void CreateEdgeColliders()
    {
        m_topEdge.points = new [] { m_topLeftCorner, m_topRightCorner };
        m_topEdge.isTrigger = true;
        m_righEdge.points = new[] { m_topRightCorner, m_bottomRightCorner };
        m_righEdge.isTrigger = true;
        m_bottomEdge.points = new[] { m_bottomRightCorner, m_bottomLeftCorner };
        m_bottomEdge.isTrigger = true;
        m_leftEdge.points = new[] { m_bottomLeftCorner, m_topLeftCorner };
        m_leftEdge.isTrigger = true;
    }
    
}

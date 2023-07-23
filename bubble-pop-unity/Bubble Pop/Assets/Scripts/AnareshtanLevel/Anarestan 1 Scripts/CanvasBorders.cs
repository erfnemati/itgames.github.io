using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBorders : MonoBehaviour
{
    private Canvas m_canvas;
    private Rigidbody2D m_rigidBody;
    private EdgeCollider2D m_leftCollider;
    private EdgeCollider2D m_rightCollider;
    //private EdgeCollider2D m_topCollider;
    private EdgeCollider2D m_bottomCollider;

    private float m_screenHeight;
    private float m_screenWidth;

    Vector2 m_topLeft, m_topRight, m_bottomLeft, m_bottomRight;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    private void Start()
    {
        m_leftCollider = gameObject.AddComponent<EdgeCollider2D>();
        m_rightCollider = gameObject.AddComponent<EdgeCollider2D>();
        //m_topCollider = gameObject.AddComponent<EdgeCollider2D>();
        m_bottomCollider = gameObject.AddComponent<EdgeCollider2D>();

        m_rigidBody = gameObject.AddComponent<Rigidbody2D>();
        m_rigidBody.bodyType = RigidbodyType2D.Kinematic;

        m_canvas = GetComponent<Canvas>();

        GetScreenDimentions();
        GetScreenCordinates();
        DrawCollider();
    }

    private void GetScreenDimentions()
    {
        m_screenHeight = m_canvas.GetComponent<RectTransform>().rect.height;
        m_screenWidth = m_canvas.GetComponent<RectTransform>().rect.width;
    }

    private void GetScreenCordinates()
    {
        m_topLeft = new Vector2(-m_screenWidth / 2, m_screenHeight / 2);
        m_topRight = new Vector2(m_screenWidth / 2, m_screenHeight / 2);
        m_bottomLeft = new Vector2(-m_screenWidth / 2, -m_screenHeight / 2);
        m_bottomRight = new Vector2(m_screenWidth / 2, -m_screenHeight / 2);
    }

    private void DrawCollider()
    {

        m_leftCollider.points = new[] { m_topLeft, m_bottomLeft };
        m_rightCollider.points = new[] { m_topRight, m_bottomRight };
        //m_topCollider.points = new[] { m_topLeft, m_topRight };
        m_bottomCollider.points = new[] { m_bottomLeft, m_bottomRight };
    }

    

    

}

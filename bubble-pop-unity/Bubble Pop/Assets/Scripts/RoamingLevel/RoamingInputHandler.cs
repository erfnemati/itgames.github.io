using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoamingInputHandler : MonoBehaviour
{
    public const string FUEL_BUBBLE_TAG = "FuelBubble";

    private bool m_isDragging = false;
    private GameObject m_touchedObject;
    private bool m_isDestroyed = false;

    public static RoamingInputHandler _instance;


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

    // Update is called once per frame
    void Update()
    {
        if (m_isDestroyed)
        {
            return;
        }
        Vector3 touchedWorldPos = new Vector3(0, 0, 0);
        if (Input.touchCount != 0)
        {
            Vector2 touchedScreenPos = Input.touches[0].position;
            touchedWorldPos = Camera.main.ScreenToWorldPoint(touchedScreenPos);

            Collider2D touchedCollider = Physics2D.OverlapPoint(touchedWorldPos);
            if (touchedCollider != null && touchedCollider.CompareTag(FUEL_BUBBLE_TAG))
            {
                m_isDragging = true;
                m_touchedObject = touchedCollider.gameObject;
            }
            else if (touchedCollider != null)
            {
                m_isDragging = false;
                m_touchedObject = null;
            }
        }

        else
        {
            m_isDragging = false;
            m_touchedObject = null;
        }

        if (m_isDragging)
        {
            m_touchedObject.transform.position = new Vector3 (touchedWorldPos.x,touchedWorldPos.y,0);
        }
    }

    public void SetIsDragging(bool isDragging)
    {
        m_isDragging = isDragging;
    }
}

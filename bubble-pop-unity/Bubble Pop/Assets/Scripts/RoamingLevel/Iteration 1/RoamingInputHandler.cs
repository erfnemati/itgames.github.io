using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoamingInputHandler : MonoBehaviour
{
    public const string SHUTTLE_PART_TAG = "ShuttlePart";
    public const string INNER_HANDLER = "InnerHandler";

    private bool m_isDragging = false;
    private bool m_isInnerHandlerDragging = false;
    private GameObject m_touchedObject;
    private bool m_isGamePaused = false;
   

    [SerializeField] HandlerController m_handlerController;

    public static RoamingInputHandler _instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


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
        if (m_isGamePaused)
        {
            return;
        }
        Vector3 touchedWorldPos = new Vector3(0, 0, 0);
        Collider2D touchedCollider = null;
        if (Input.touchCount != 0)
        {
            Vector2 touchedScreenPos = Input.touches[0].position;
            touchedWorldPos = Camera.main.ScreenToWorldPoint(touchedScreenPos);

            touchedCollider = Physics2D.OverlapPoint(touchedWorldPos);
            if (touchedCollider != null && (touchedCollider.CompareTag(SHUTTLE_PART_TAG) || touchedCollider.CompareTag("RoamingBubble")))
            {
                if(touchedCollider.GetComponent<RoamingBubble>() != null)
                {
                    touchedCollider.GetComponent<RoamingBubble>().KillCurrentTween();
                }
                m_isDragging = true;
                m_isInnerHandlerDragging = false;
                m_touchedObject = touchedCollider.gameObject;
            }
            else if (touchedCollider != null && touchedCollider.CompareTag(INNER_HANDLER))
            {
                m_isDragging = false;
                m_isInnerHandlerDragging = true;
                m_touchedObject = touchedCollider.gameObject;
            }

            else if (touchedCollider != null)
            {
                m_isDragging = false;
                m_isInnerHandlerDragging = false;
                m_touchedObject = null;
            }
        }

        else
        {
            m_isDragging = false;
            m_isInnerHandlerDragging = false;
            m_touchedObject = null;
        }

        if (m_isDragging)
        {
            MoveFuelBubble(touchedWorldPos);
        }

        else if (m_isInnerHandlerDragging)
        {
            Vector3 tempRotation = new Vector3(touchedWorldPos.x, touchedWorldPos.y, 0);
            RotateHandler(tempRotation);
        }

    }

    private void MoveFuelBubble(Vector3 touchedWorldPos)
    {
        m_touchedObject.transform.position = new Vector3(touchedWorldPos.x, touchedWorldPos.y, 0);

        CircleCollider2D shuttlePartCollider = m_touchedObject.GetComponent<CircleCollider2D>();

        float halfOfDropWidth = shuttlePartCollider.radius * (m_touchedObject.transform.localScale.x);
        float halfOfDropHeight = shuttlePartCollider.radius * (m_touchedObject.transform.localScale.y);

        Vector3 newWorldPos = new Vector3(0, 0, 0);

        //For left edge : 
        Vector3 tempPos = m_touchedObject.transform.position - new Vector3(halfOfDropWidth, 0f, 0f);
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(tempPos);

        if (viewportPos.x <= Mathf.Epsilon)
        {
            newWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0.01f, viewportPos.y, viewportPos.z));
            m_touchedObject.transform.position = newWorldPos + new Vector3(halfOfDropWidth, 0, 0);
        }

        //For right edge : 
        tempPos = m_touchedObject.transform.position + new Vector3(halfOfDropWidth, 0f, 0f);
        viewportPos = Camera.main.WorldToViewportPoint(tempPos);

        if (viewportPos.x - 1f >= Mathf.Epsilon)
        {
            newWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0.99f, viewportPos.y, viewportPos.z));
            m_touchedObject.transform.position = newWorldPos - new Vector3(halfOfDropWidth, 0, 0);
        }

        //For top edge : 
        tempPos = m_touchedObject.transform.position + new Vector3(0, halfOfDropHeight, 0);
        viewportPos = Camera.main.WorldToViewportPoint(tempPos);
        if (viewportPos.y - 1f >= Mathf.Epsilon)
        {
            newWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, 0.99f, viewportPos.z));
            m_touchedObject.transform.position = newWorldPos - new Vector3(0, halfOfDropHeight, 0);
        }
        //For bottom edge : 
        tempPos = m_touchedObject.transform.position - new Vector3(0, halfOfDropHeight, 0);
        viewportPos = Camera.main.WorldToViewportPoint(tempPos);
        if (viewportPos.y <= Mathf.Epsilon)
        {
            newWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, 0.01f, viewportPos.z));
            m_touchedObject.transform.position = newWorldPos + new Vector3(0, halfOfDropHeight, 0);

        }
    }

    private void RotateHandler(Vector3 position)
    {
        m_handlerController.RotateTowards(position);
    }
    public void SetIsDragging(bool isDragging)
    {
        m_isDragging = isDragging;
    }

    public void SetIsGamePuased(bool isGamePaused)
    {
        m_isGamePaused = isGamePaused;
    }
}

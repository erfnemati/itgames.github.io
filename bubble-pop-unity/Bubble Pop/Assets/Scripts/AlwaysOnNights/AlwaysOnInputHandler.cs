using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOnInputHandler : MonoBehaviour
{
    private bool m_isGamePuased = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isGamePuased)
        {
            return;
        }
        GetUserInput();
    }

    private void GetUserInput()
    {
        if (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector3 touchedWorldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchedWorldPos);

            if (touchedCollider != null)
            {
                if (touchedCollider.CompareTag("AddedValueBubble"))
                {
                    touchedCollider.GetComponent<AddedValueBubble>().Pop();
                }
            }
        }
    }

    public void SetGamePauseState(bool isGamePaused)
    {
        m_isGamePuased = isGamePaused;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const string ANAR_OBJECT_NAME = "AnarObject";
    const string VERTICAL_SCREEN_BORDER = "ScreenBorder";



    private bool m_goingRight = false;
    private bool m_goingLeft = false;
    private bool m_staying = false;



    private Vector3 m_enteringPos;

    [SerializeField] float m_speed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(VERTICAL_SCREEN_BORDER))
        {
            m_enteringPos = this.gameObject.transform.position;
            this.gameObject.transform.position = m_enteringPos;
           
        }

        if (collision.gameObject.CompareTag(ANAR_OBJECT_NAME))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(VERTICAL_SCREEN_BORDER))
        {
            transform.position = m_enteringPos;
        }
    }


    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        if(m_goingRight)
        {
            direction = new Vector3(1, 0, 0);
        }
        else if (m_goingLeft)
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if (m_staying)
        {
            return;
        }

        transform.position = transform.position + (direction * m_speed * Time.deltaTime);
    }

    

    public void GoingRight()
    {
        
        m_goingLeft = false;
        m_goingRight = true;
        m_staying = false;
    }

    public void GoingLeft()
    {
        
        m_goingLeft = true;
        m_goingRight = false;
        m_staying = false;
    }

    public void Staying()
    {
        
        m_goingLeft = false;
        m_goingRight = false;
        m_staying = true;
    }
}

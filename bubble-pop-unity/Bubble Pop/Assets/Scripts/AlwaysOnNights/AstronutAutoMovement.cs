using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronutAutoMovement : MonoBehaviour
{
    //Movement parameters : 

    private Vector3 m_direction = Vector3.left;
    [SerializeField] float m_speed;

    [SerializeField] AstronutUiController m_UiController;
    
    
    private void Update()
    {
        Vector3 thisFrameMovement = m_speed * m_direction * Time.deltaTime;
        transform.position += thisFrameMovement;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelBoundry"))
        {
            ChangeDirection();
        }

        
    }

    private void ChangeDirection()
    {
        m_direction = -m_direction;

        if (m_direction.x < 0)
        {
            m_UiController.GoingLeft();
        }
        else
        {
            m_UiController.GoingRight();
        }
    }

    


}

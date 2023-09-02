using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronutAutoMovement : MonoBehaviour
{
    //Movement parameters : 

    private Vector3 m_direction = Vector3.right;
    [SerializeField] float m_speed;
    
    
    private void Update()
    {
        Vector3 thisFrameMovement = m_speed * m_direction * Time.deltaTime;
        transform.position += thisFrameMovement;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelBoundry"))
        {
            m_direction = -m_direction;
        }
        
    }


}

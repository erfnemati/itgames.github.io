using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBubble : MonoBehaviour
{

    const string SCREEN_BORDER = "ScreenBorder";

    [SerializeField] float m_speed;
    private Vector3 m_movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        m_movementDirection = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);
        Fly();
        
    }

    


    private void Fly()
    {
        GetComponent<Rigidbody2D>().velocity = (m_movementDirection).normalized * m_speed;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(SCREEN_BORDER))
        {
            Debug.Log("Destroying");
            Destroy(this.gameObject);
        }
    }




}

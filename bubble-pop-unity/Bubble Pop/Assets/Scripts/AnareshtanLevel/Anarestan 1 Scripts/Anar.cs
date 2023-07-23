using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anar : MonoBehaviour
{
    const string CANVAS_TAG = "ScreenBorder";

    [SerializeField] float m_speed;
    private Vector2 m_direction = new Vector2(0, -1);


    private void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        Vector2 movement = m_direction * m_speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(movement.x , movement.y , 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(CANVAS_TAG))
        {
            Debug.Log("Exiting");
            Destroy(this.gameObject);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    const string CANVAS_TAG = "ScreenBorder";

    [SerializeField] float m_speed;

    private void Start()
    {
        m_speed = Random.Range(4f, 7f);
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {

        transform.position = transform.position + (m_speed * Time.deltaTime * new Vector3(0, -1, 0));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(CANVAS_TAG))
        {
            Destroy(this.gameObject);
        }
    }

}

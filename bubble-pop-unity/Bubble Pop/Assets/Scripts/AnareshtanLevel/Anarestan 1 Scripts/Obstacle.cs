using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    const string CANVAS_TAG = "ScreenBorder";

    [SerializeField] float m_speed;

    private void Start()
    {
        int currentDifficulty = LevelManager.m_instance.GetDifficultyLevel();
        if (currentDifficulty == 1)
        {
            m_speed = Random.Range(1f, 2f);
        }
        else if  (currentDifficulty == 2)
        {
            m_speed = Random.Range(3f, 5f);
        }
        else
        {
            m_speed = Random.Range(4f, 6f);
        }
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

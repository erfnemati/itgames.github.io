using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Anar : MonoBehaviour
{
    const string CANVAS_TAG = "ScreenBorder";

    [SerializeField] float m_speed;
    //[SerializeField] TMP_Text m_anarText;
   
    private Vector2 m_direction = new Vector2(0, -1);

    private void Start()
    {
        SetSpeed();
    }

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
            Destroy(this.gameObject);

        }
    }

    private void SetSpeed()
    {
        int levelDif =  LevelManager.m_instance.GetDifficultyLevel();
        if (levelDif == 1)
        {
            m_speed = Random.Range(1f, 2f);
        }
        else if(levelDif == 2)
        {
            m_speed = Random.Range(2f, 4f);
        }
        else
        {
            m_speed = Random.Range(4f, 6f);  
        }
    }
}

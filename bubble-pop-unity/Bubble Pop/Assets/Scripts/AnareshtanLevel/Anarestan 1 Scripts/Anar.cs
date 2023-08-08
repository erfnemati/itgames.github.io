using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Anar : MonoBehaviour
{
    const string CANVAS_TAG = "ScreenBorder";

    [SerializeField] float m_speed;
    //[SerializeField] TMP_Text m_anarText;
    [SerializeField] List<string> m_slogans = new List<string>();
    private Vector2 m_direction = new Vector2(0, -1);

    private void Start()
    {
        m_speed = Random.Range(1f, 3f);
        SetAnarText();
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

    private void SetAnarText()
    {
        int randomSloganIndex = Random.Range(0, m_slogans.Count);
       // m_anarText.text =  m_slogans[randomSloganIndex];
    }
}

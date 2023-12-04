using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BubbleTap : MonoBehaviour
{
    [SerializeField]int m_numOfTaps = 10;
    [SerializeField] GameObject m_TapText;

    [SerializeField] GameObject m_anarItem;
    [SerializeField] TMP_Text m_titleText;
    void Start()
    {
        
    }

    void Update()
    {
        CheckInput();
    }

    private void  CheckInput()
    {
        if (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Collider2D selectedObject = Physics2D.OverlapPoint(new Vector2(worldPos.x,worldPos.y));
            if ( selectedObject != null && selectedObject.CompareTag("Bubble") )
            {
                Tap();
                Debug.Log("Bubble Tapped : " + m_numOfTaps);
            }

        }
    }


    public void Tap()
    {
        m_numOfTaps--;
        m_TapText.SetActive(false);
        if (m_numOfTaps <= 0)
        {
            Pop();
            
        }
    }

    private void Pop()
    {
        Instantiate(m_anarItem, this.transform.position, Quaternion.identity);
        m_titleText.text = "Tap To Grab";
        Destroy(this.gameObject);

        
    }
}

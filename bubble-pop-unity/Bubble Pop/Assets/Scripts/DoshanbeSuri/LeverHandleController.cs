using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeverHandleController : MonoBehaviour
{
    [SerializeField] WheelOfLuckController m_wheelOfLuck;
    [SerializeField] GameObject m_levelOneButton;
    [SerializeField] GameObject m_levelTwoButton;
    [SerializeField] GameObject m_levelThreeButton;
    [SerializeField] GameObject m_levelFourButton;
    [SerializeField] Sprite m_turnedOnLampSprite;
    [SerializeField] Sprite m_turnedOffLampSprite;

    [SerializeField] Transform m_maxHeight;
    [SerializeField] Transform m_minHeigth;

    [SerializeField] Vector3 m_initialPos;

    [SerializeField] float m_numOfTries = 3;
    [SerializeField] GameObject m_firstGameObject;
    [SerializeField] GameObject m_secGameObject;
    [SerializeField] GameObject m_thirdGameObject;

    private bool m_isDragging = false;
    private bool m_isLeverActive = true;
    private bool m_isTutorialTurnedOff = false;
    private bool m_onCollider = false;
    private float m_collisionTime = 0.0f;


    private void Start()
    {
        m_initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    private void Update()
    {
        if (m_isLeverActive)
        {
            GetInput();
        }

    }

    private void GetInput()
    {
        
        Vector2 touchedWorldPos = Vector2.zero;
        Collider2D touchedCollider = null;
        if (Input.touchCount != 0)
        {
            touchedWorldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            touchedCollider = Physics2D.OverlapPoint(touchedWorldPos);
            if (touchedCollider != null && touchedCollider.CompareTag("Lever"))
            {
                if (m_isTutorialTurnedOff == false)
                {
                    m_isTutorialTurnedOff = true;
                    DoshanbeSuriTutorialManager._instance.DeactivateTutorials();
                }
                m_isDragging = true;
            }
            
        }

        else
        {
            m_isDragging = false;
        }

        if (m_isDragging)
        {

            MoveLever(touchedCollider, touchedWorldPos);
        }

        
    }


    private void MoveLever(Collider2D touchedCollider, Vector3 targetPos)
    {
        Vector3 initialPos = transform.position;
        if (targetPos.y > m_maxHeight.position.y)
        {
            transform.position = new Vector3(initialPos.x, m_maxHeight.position.y, initialPos.z);
        }

        else if (targetPos.y < m_minHeigth.position.y)
        {
            transform.position = new Vector3(initialPos.x, m_minHeigth.position.y, initialPos.z);
        }

        else
        {
            transform.position = new Vector3(initialPos.x, targetPos.y, initialPos.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelOneButton"))
        {
            m_levelOneButton.GetComponent<SpriteRenderer>().sprite = m_turnedOnLampSprite;
        }
        else if (collision.CompareTag("LevelTwoButton"))
        {
            m_levelTwoButton.GetComponent<SpriteRenderer>().sprite = m_turnedOnLampSprite;
        }
        else if (collision.CompareTag("LevelThreeButton"))
        {
            m_levelThreeButton.GetComponent<SpriteRenderer>().sprite = m_turnedOnLampSprite;
        }
        else if (collision.CompareTag("LevelFourButton"))
        {
            m_levelFourButton.GetComponent<SpriteRenderer>().sprite = m_turnedOnLampSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelOneButton"))
        {
            m_levelOneButton.GetComponent<SpriteRenderer>().sprite = m_turnedOffLampSprite;
        }
        else if (collision.CompareTag("LevelTwoButton"))
        {
            m_levelTwoButton.GetComponent<SpriteRenderer>().sprite = m_turnedOffLampSprite;
        }
        else if (collision.CompareTag("LevelThreeButton"))
        {
            m_levelThreeButton.GetComponent<SpriteRenderer>().sprite = m_turnedOffLampSprite;
        }
        else if (collision.CompareTag("LevelFourButton"))
        {
            m_levelFourButton.GetComponent<SpriteRenderer>().sprite = m_turnedOffLampSprite;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_isDragging)
        {
            return;
        }

        if (m_isLeverActive == false || m_numOfTries <= 0)
        {
            return;
        }

        
        SetLeverState(false);
        if (collision.CompareTag("LevelOneButton"))
        {
            ReduceNumOfTries();
            m_wheelOfLuck.Spin(1);
        }
        else if (collision.CompareTag("LevelTwoButton"))
        {
            ReduceNumOfTries();
            m_wheelOfLuck.Spin(2);
        }
        else if (collision.CompareTag("LevelThreeButton"))
        {
            ReduceNumOfTries();
            m_wheelOfLuck.Spin(3);
        }
        else if (collision.CompareTag("LevelFourButton"))
        {
            ReduceNumOfTries();
            m_wheelOfLuck.Spin(4);
        }
    }

    private void ReduceNumOfTries()
    {
        m_numOfTries--;
        if (m_numOfTries == 2)
        {
            m_firstGameObject.GetComponent<SpriteRenderer>().sprite = m_turnedOffLampSprite;
        }
        else if (m_numOfTries == 1)
        {
            m_secGameObject.GetComponent<SpriteRenderer>().sprite = m_turnedOffLampSprite;
        }
        else if (m_numOfTries == 0)
        {
            m_thirdGameObject.GetComponent<SpriteRenderer>().sprite = m_turnedOffLampSprite;
        }
    }

    private void SetLeverState(bool leverState)
    {
        m_isLeverActive = leverState;
    }

    public void ResetLever()
    {
        transform.position = m_initialPos;
        m_isLeverActive = true;
    }


}

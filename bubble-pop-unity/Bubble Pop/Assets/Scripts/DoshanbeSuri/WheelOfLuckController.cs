using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelOfLuckController : MonoBehaviour
{
    [SerializeField] int m_numOfGifts;
    [SerializeField] int m_targetGiftIndex;
    [SerializeField] float m_rotationSpeed;
    [SerializeField] List<Gift> m_giftList = new List<Gift>();
    [SerializeField] List<Button> m_wheelOfLuckButtons = new List<Button>();

    //Parameters for speed caculations : 
    [SerializeField] float m_initialSpeed;
    private float m_acceleration;
    private float m_timeStamp;

    private float m_angleForEachGift;

    private const float CIRCLE = 360.0f; //Think about readOnly vs Const

    private bool m_isSpinning = false;
    private bool m_isGamePuased = false;
    private int m_amountOfRotation = 0;
    private float m_passedRotation = 0.0f;
    private float m_angleOffset;
    private Vector3 m_rotationVector = Vector3.forward;

    private void Update()
    {
        if (m_isGamePuased)
            return;

        if(m_isSpinning)
        {
            Spin();
        }
    }

    private void Start()
    {
        m_angleOffset = (CIRCLE / m_numOfGifts) / 2;
        m_angleForEachGift = CIRCLE / m_numOfGifts;  
    }

    public void Spin()
    {
        m_timeStamp += Time.deltaTime;
        SetRotationSpeed(m_timeStamp);
        float thisFrameRotation = m_rotationSpeed * Time.deltaTime;
        m_passedRotation += thisFrameRotation;
        transform.Rotate(thisFrameRotation * m_rotationVector);
        Debug.Log("Passed rotation is : " + m_passedRotation);
        if (m_passedRotation - m_amountOfRotation >= -2f)
        {
            Debug.Log("Time to stop");
            m_isSpinning = false;
            m_passedRotation = 0.0f;
            if(IsGiftTaken())
            {
                Invoke(nameof(FinishGame), 2f);
            }
            else
            {
                Invoke(nameof(Reset), 3f);
            }
        }
    }

    private void InitialiseSpeedFormula()
    {
        m_acceleration = (-Mathf.Pow(m_initialSpeed, 2)) / (2 * m_amountOfRotation);
    }

    private void SetRotationSpeed(float time)
    {
        m_rotationSpeed = m_acceleration * time + m_initialSpeed;
        if (m_rotationSpeed < 0)
        {
            m_rotationSpeed = 0;
        }
        
    }

    private void Reset()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        ActivateButtons();
    }

    private void ActivateButtons()
    {
        foreach (Button temp in m_wheelOfLuckButtons)
        {
            temp.interactable = true;
        }
    }

    public void Spin(int level =1)
    {
        int numOfCircles = Random.Range(1, 4);
        float delta = 0.0f;
        
        if (level == 1)
        {
            delta = -180;

        }
        else if (level == 2)
        {
            delta = -90;
        }
        else if (level == 3)
        {
            delta = 0;
        }
        else
        {
            delta = 90;
        }

        float angleBackOffset = (m_targetGiftIndex * m_angleForEachGift) + delta;

        if (angleBackOffset < Mathf.Epsilon)
        {
            angleBackOffset += 360f;
        }

        if (angleBackOffset % m_angleForEachGift == 0)
        {
            angleBackOffset += m_angleOffset;
        }


        m_amountOfRotation = (int)angleBackOffset + (int)(numOfCircles * CIRCLE);
        InitialiseSpeedFormula();
        m_timeStamp = 0.0f;
        m_isSpinning = true;
        
    }

    public void DeactivateButtons()
    {
        foreach(Button temp in m_wheelOfLuckButtons)
        {
            temp.interactable = false;
        }
    }

    private bool IsGiftTaken()
    {
        float remainderOfCircle = m_amountOfRotation % CIRCLE;
        int giftIndex = (int)remainderOfCircle / (int)m_angleForEachGift;
        
        if(giftIndex == m_targetGiftIndex)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FinishGame()
    {
        DoshanbeSuriGameManager._instance.FinishGame();
    }

    public void SetIsGamePaused(bool pauseState)
    {
        m_isGamePuased = pauseState;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOfLuckController : MonoBehaviour
{
    [SerializeField] int m_numOfGifts;
    [SerializeField] int m_targetGiftIndex;
    [SerializeField] float m_rotationSpeed;
    [SerializeField] List<Gift> m_giftList = new List<Gift>();

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
        float thisFrameRotation = m_rotationSpeed * Time.deltaTime;
        m_passedRotation += thisFrameRotation;
        transform.Rotate(thisFrameRotation * m_rotationVector);
        if (m_passedRotation - m_amountOfRotation >= Mathf.Epsilon)
        {
            m_isSpinning = false;
            m_passedRotation = 0.0f;
            GetGift();
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
        Debug.Log(m_amountOfRotation);
        m_isSpinning = true;
    }

    public void SpinLevelOne()
    {
        int numOfCircles = Random.Range(1, 4);
        float angleBackOffset = (m_targetGiftIndex * m_angleForEachGift) - 180f;
        if (angleBackOffset < Mathf.Epsilon)
        {
            angleBackOffset += 360f;
        }

        if (angleBackOffset % m_angleForEachGift == 0)
        {
            angleBackOffset += m_angleOffset;
        }


        m_amountOfRotation = (int)angleBackOffset + (int)(numOfCircles * CIRCLE);
        Debug.Log(m_amountOfRotation);
        m_isSpinning = true;
    }

    private void StartSpin()
    {
        m_passedRotation = 0.0f;
        m_amountOfRotation =  Random.Range(500, 1000);
        int tempRemainder = (m_amountOfRotation % 360) % m_numOfGifts;
        if (tempRemainder == 0)
        {
            m_amountOfRotation += (int)m_angleOffset;
        }

        Debug.Log(m_amountOfRotation);
        m_isSpinning = true;

    }

    private void GetGift()
    {
        float remainderOfCircle = m_amountOfRotation % CIRCLE;
        int giftIndex = (int)remainderOfCircle / (int)m_angleForEachGift;
        Debug.Log("Index of gift list is : " + giftIndex);
        if(giftIndex == m_targetGiftIndex)
        {
            Debug.Log(m_giftList[giftIndex].GetGift());
            
        }
        else
        {
            Debug.Log("Try again");
        }
    }

    public void SetIsGamePaused(bool pauseState)
    {
        m_isGamePuased = pauseState;
    }

    
}

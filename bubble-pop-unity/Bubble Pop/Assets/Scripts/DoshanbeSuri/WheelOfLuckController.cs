using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOfLuckController : MonoBehaviour
{
    [SerializeField] int m_numOfGifts;
    [SerializeField] int m_targetGiftIndex;
    [SerializeField] float m_rotationSpeed;

    private float m_angleForEachGift;

    private const float CIRCLE = 360.0f; //Think about readOnly vs Const

    private bool m_isSpinning = false;
    private float m_amountOfRotation = 0.0f;
    private float m_passedRotation = 0.0f;
    private Vector3 m_rotationVector = Vector3.forward;

    private void Update()
    {
        if(m_isSpinning)
        {
            Spin();
        }
    }

    private void Start()
    {
        m_angleForEachGift = CIRCLE / m_numOfGifts;
        StartSpin();
    }

    public void Spin()
    {
        float thisFrameRotation = m_rotationSpeed * Time.deltaTime;
        m_passedRotation += thisFrameRotation;
        transform.Rotate(thisFrameRotation * m_rotationVector);
        if (m_passedRotation - m_amountOfRotation >= Mathf.Epsilon)
        {
            m_isSpinning = false;
            GetGift();
        }
    }

    private void StartSpin()
    {
        m_passedRotation = 0.0f;
        m_amountOfRotation =  Random.Range(500, 1000);
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
            Debug.Log("You have won");
        }
        else
        {
            Debug.Log("Try again");
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBubble : MonoBehaviour
{
    [SerializeField] float m_rotationSpeed;
    private Vector3 m_rotationDirection;

    private void Start()
    {
        SetRotationDirection();
    }

    private void Update()
    {
        transform.Rotate(m_rotationDirection, m_rotationSpeed * Time.deltaTime);
    }

    private void SetRotationDirection()
    {
        int random = Random.Range(1, 3);
        if (random == 1)
        {
            m_rotationDirection = new Vector3(0, 0, 1);
        }
        else
        {
            m_rotationDirection = new Vector3(0, 0, -1);
        }
    }
}

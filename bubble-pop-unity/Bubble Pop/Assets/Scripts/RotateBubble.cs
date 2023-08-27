using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBubble : MonoBehaviour
{
    [SerializeField] float m_rotationSpeed;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), m_rotationSpeed * Time.deltaTime);
    }
}

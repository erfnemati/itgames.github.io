using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWaterBubble : MonoBehaviour
{
    private Vector3 m_startingPos;
    private float startingDegree = 0.0f;
    [SerializeField] float m_distance;
    [SerializeField] float m_translationSpeed;
    [SerializeField] Vector3 m_direction;
    // Start is called before the first frame update
    void Start()
    {
        m_startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        startingDegree = startingDegree + (m_translationSpeed * Time.deltaTime);
        transform.position = m_startingPos + (Mathf.Sin(startingDegree * Mathf.Deg2Rad) * m_direction.normalized * m_distance);
    }
}

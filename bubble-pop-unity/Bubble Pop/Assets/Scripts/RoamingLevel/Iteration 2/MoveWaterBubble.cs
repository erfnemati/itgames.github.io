using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveWaterBubble : MonoBehaviour
{
    private Vector3 m_startingPos;
    private float startingDegree = 0.0f;
    [SerializeField] float m_distance;
    [SerializeField] float m_translationSpeed;
    [SerializeField] Vector3 m_direction;

    // Start is called before the first frame update
    

    private void OnEnable()
    {
        m_startingPos = transform.position;
        AnimateGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        startingDegree = startingDegree + (m_translationSpeed * Time.deltaTime);
        transform.position = m_startingPos + (Mathf.Sin(startingDegree * Mathf.Deg2Rad) * m_direction.normalized * m_distance);
    }

    public void SetDirection(Vector3 direction)
    {
        m_direction = direction;
    }

    public void SetSpeed(float speed)
    {
        m_translationSpeed = speed;
    }

    public void SetDistance(float distance)
    {
        m_distance = distance;
    }

    private void AnimateGeneration()
    {
        transform.DOScale(0.3f, 0.5f);
    }
}

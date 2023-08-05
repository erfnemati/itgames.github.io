using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_anar;
    [SerializeField] GameObject m_obstacle;
    
    private GameObject m_lastGeneratedObject;
    

    [SerializeField] float m_minDistance;
    [SerializeField] float m_maxDistance;
    [SerializeField] Transform m_TopOfScreen;
    private float m_distanceFromLastAnar;

    private void Start()
    {
        GenerateAnar();
    }
    private void Update()
    {
        if (m_lastGeneratedObject.transform.position.y <= m_distanceFromLastAnar)
        {
            GenerateAnar();
        }
    }
    private void GenerateAnar()
    {
        float xPosInViewPort = Random.Range(0.2f, 0.7f);
        float xPosInWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(xPosInViewPort, 0, 0)).x;
        Vector3 toBeGeneratedPos = new Vector3(xPosInWorldPoint, m_TopOfScreen.transform.position.y, 0);

        int obsOrAnar = Random.Range(1, 6);
        if (obsOrAnar >3)
        {
            xPosInViewPort = Random.Range(0.3f, 0.7f);
            xPosInWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(xPosInViewPort, 0, 0)).x;
            toBeGeneratedPos = new Vector3(xPosInWorldPoint, m_TopOfScreen.transform.position.y, 0);
            m_lastGeneratedObject = Instantiate(m_obstacle, toBeGeneratedPos, Quaternion.identity);
        }
        else
        {
            m_lastGeneratedObject = Instantiate(m_anar, toBeGeneratedPos, Quaternion.identity);
        }
        

        float toBeGeneratedPoint = Random.Range(0.5f, 0.7f);
        m_distanceFromLastAnar = Camera.main.ViewportToWorldPoint(new Vector3(0f, toBeGeneratedPoint, 0)).y;
    }


    


}

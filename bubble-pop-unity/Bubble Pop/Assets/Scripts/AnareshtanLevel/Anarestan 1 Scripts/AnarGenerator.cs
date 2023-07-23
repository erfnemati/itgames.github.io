using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnarGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_anar;
    [SerializeField] string m_text1;
    [SerializeField] string m_text2;
    [SerializeField] string m_text3;

    private GameObject m_lastGeneratadAnar;
    private GameObject m_currentGeneratedAnar;

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
        if (m_lastGeneratadAnar.transform.position.y <= m_distanceFromLastAnar)
        {
            GenerateAnar();
        }
    }
    private void GenerateAnar()
    {
        float xPosInViewPort = Random.Range(0.2f, 0.8f);
        float xPosInWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(xPosInViewPort, 0, 0)).x;
        Vector3 toBeGeneratedPos = new Vector3(xPosInWorldPoint, m_TopOfScreen.transform.position.y, 0);

        m_lastGeneratadAnar = Instantiate(m_anar, toBeGeneratedPos, Quaternion.identity);
        float toBeGeneratedPoint = Random.Range(0.3f, 0.7f);
        m_distanceFromLastAnar = Camera.main.ViewportToWorldPoint(new Vector3(0f, toBeGeneratedPoint, 0)).y;
        Debug.Log("Distance is : " + m_distanceFromLastAnar);
    }


}

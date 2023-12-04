using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleGenerator : MonoBehaviour
{
   // [SerializeField] GameObject m_anar;
    [SerializeField] GameObject m_obstacle;
    [SerializeField] GameObject[] anarlist = new GameObject[6];
    private int m_typeOfAnar = 0;

    private GameObject m_lastGeneratedObject;

    public int m_numOfGeneratedAnar = 0;
    [SerializeField] int m_maxNumOfGeneratedAnar;
    [SerializeField] float m_minDistance;
    [SerializeField] float m_maxDistance;
    [SerializeField] Transform m_TopOfScreen;
    private float m_distanceFromLastAnar;

    public bool m_isLevelFin;
    public AudioSource dropobc, dropanar;

    private void Start()
    {
        LevelManager.m_instance.ActivateAnarPanel();
        GenerateAnar();
    }
    private void Update()
    {
        if (m_isLevelFin)
        {
            return;
        }
        if (  m_numOfGeneratedAnar > m_maxNumOfGeneratedAnar && FindObjectOfType<Anar>() == null )
        {
            LevelManager.m_instance.EndAnarGeneration();
            m_isLevelFin = true;
            return;
        }
        if (m_lastGeneratedObject.transform.position.y <= m_distanceFromLastAnar)
        {
            GenerateAnar();
        }
    }
    private void GenerateAnar()
    {

        if (m_numOfGeneratedAnar > m_maxNumOfGeneratedAnar )
        {
            return;
        }
        float xPosInViewPort = Random.Range(0.2f, 0.7f);
        float xPosInWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(xPosInViewPort, 0, 0)).x;
        Vector3 toBeGeneratedPos = new Vector3(xPosInWorldPoint, m_TopOfScreen.transform.position.y, 0);

        int obsOrAnar = Random.Range(1, 6);
        if (obsOrAnar > 2)
        {
            xPosInViewPort = Random.Range(0.3f, 0.7f);
            xPosInWorldPoint = Camera.main.ViewportToWorldPoint(new Vector3(xPosInViewPort, 0, 0)).x;
            toBeGeneratedPos = new Vector3(xPosInWorldPoint, m_TopOfScreen.transform.position.y, 0);
            m_lastGeneratedObject = Instantiate(m_obstacle, toBeGeneratedPos, Quaternion.identity);
            dropobc.Play();


        }
        else  
        {
            m_lastGeneratedObject = Instantiate(anarlist[m_typeOfAnar], toBeGeneratedPos, Quaternion.identity);
            m_typeOfAnar = (m_typeOfAnar + 1) % anarlist.Length;
            m_numOfGeneratedAnar++;
            dropanar.Play();
        }
       
        float toBeGeneratedPoint = Random.Range(0.5f, 0.7f);
        m_distanceFromLastAnar = Camera.main.ViewportToWorldPoint(new Vector3(0f, toBeGeneratedPoint, 0)).y;

    }





}

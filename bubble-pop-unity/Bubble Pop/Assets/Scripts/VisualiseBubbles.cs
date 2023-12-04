using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class VisualiseBubbles : MonoBehaviour
{
    [SerializeField] List<Transform> m_transformList = new List<Transform>();
    [SerializeField] List<GameObject> m_generatedCircles = new List<GameObject>();
    [SerializeField] GameObject m_circleObject;

    [SerializeField] bool m_startDebugging = false;
    [SerializeField] bool m_startGenerating = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (m_startGenerating == true)
        {
            GenerateCircles();
        }

        if (m_startDebugging)
        {
            UpdatePos();
        }
        
    }

    private void UpdatePos()
    {
        for(int i = 0; i < m_generatedCircles.Count; i++)
        {
            m_generatedCircles[i].transform.position = m_transformList[i].position;
        }
    }

    private void GenerateCircles()
    {
        m_generatedCircles.Clear();
        for (int i = 0; i < m_transformList.Count; i++)
        {
            m_generatedCircles.Add(Instantiate(m_circleObject, m_transformList[i].position, Quaternion.identity));
            m_startGenerating = false;
        }
    }
}
